using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ImplOfRepository;
using DearDreamModels;

namespace RecomConsole
{
    public class UserBehaviorSolution
    {
        private SqlDbContext db = new SqlDbContext();

        /// <summary>
        /// 对指定时间域的用户行为统计建立用户兴趣模型
        /// </summary>
        /// <param name="beginTime">起始时间</param>
        /// <param name="endTime">终点时间</param>
        /// <returns>受影响的用户行为数目</returns>
        public int AddUpBehaviorBetween(DateTime beginTime, DateTime endTime)
        {
            IEnumerable<UserBehavior> userBehaviors = db.UserBehaviors.Where(
                u => DateTime.Compare(u.OccurTime, beginTime) > 0 &&
                    DateTime.Compare(u.OccurTime, endTime) < 0);
            IEnumerable<UserInterest> userInterets = db.UserInterets.Where
                (u => userBehaviors.Any(o => o.UserId == u.UserId));
            IEnumerable<User> users = db.Users.Where
                (u => userBehaviors.Any(o => o.UserId == u.Id));
            //Core:对用户行为进行评分汇总构建用户兴趣模型权重
            foreach (var u in users)
            {
                foreach (var newsType in db.NewsCategorys)
                {
                    double sum = 0;
                    foreach (var ut in db.TBahaviorNames)
                    {
                        var ubs = userBehaviors.Where
                            (o => o.UserId == u.Id && o.BehaviorName
                                == ut.BehaviorName && db.NewsContainer.Find(o.NewsId).Category == newsType.Name);
                        sum += ubs.Count() * ut.Weight;
                    }
                    //var utest = userInterets.Where(o => o.UserId == u.Id && o.NewsId == newsType.Id);

                    var ui = userInterets.Single(o => o.UserId == u.Id && o.NewsId == newsType.Id);
                    ui.CurentWeight = ui.CurentWeight == 0 ? sum : sum + ui.CurentWeight;
                }
            }
            //更新兴趣模型比重
            UpdateInterestProportion(users, ref userInterets);
            db.SaveChanges();
            return userBehaviors.Count();
        }

        /// <summary>
        /// 对指定时间至今的用户行为统计建立用户兴趣模型
        /// </summary>
        /// <param name="beginTime">起始时间</param>
        /// <returns>受影响的用户行为数目</returns>
        public int AddUpBehaviorFrom(DateTime beginTime)
        {
            return AddUpBehaviorBetween(beginTime, DateTime.Now);
        }

        /// <summary>
        /// 对尚未更新的用户新闻统计，更新兴趣模型
        /// </summary>
        /// <returns>受影响的用户行为数目</returns>
        public int UpdateBehaviorToLateset()
        {
            var dt = db.UserInterets.Max(o => o.UpdateTime);
            if (dt == null)
                throw new Exception("DateTime Null Value!");
            return AddUpBehaviorFrom(dt);
        }

        /// <summary>
        /// 更新指定用户的兴趣模型各类新闻比重
        /// </summary>
        /// <param name="users">指定用户集合</param>
        /// <param name="userInterests">用户兴趣模型集合</param>
        protected void UpdateInterestProportion(IEnumerable<User> users, ref IEnumerable<UserInterest> userInterests)
        {
            foreach (var u in users)
            {
                var uis = userInterests.Where(o => o.UserId == u.Id);
                double sum = 0;
                foreach (var ui in uis)
                {
                    sum += ui.CurentWeight;
                }
                foreach (var ui in uis)
                {
                    ui.Proportion = ui.CurentWeight / sum;
                    ui.UpdateTime = DateTime.Now;
                }
            }
        }
    }
}
