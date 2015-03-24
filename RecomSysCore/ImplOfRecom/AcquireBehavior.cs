using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RecomSysCore.ExpansionHelper;
using DearDreamModels;
using DearDreamDAL;
using ImplOfRepository;
using Ninject;

namespace RecomSysCore.ImplOfRecom
{
    public class AcquireBehavior:IAcquireBehavior
    {
        /// <summary>
        /// 用户新闻仓库类
        /// </summary>
        [Ninject.Inject]
        IUserBehaviorRepository _userBehaviorRepository { get; set; }
        
        /// <summary>
        /// 控制器与用户行为名称映射
        /// </summary>
        public readonly IDictionary<string, string> BehaivorControllerMap;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AcquireBehavior()
        {
            BehaivorControllerMap = new Dictionary<string, string>();
            BehaivorControllerMap.Add("SingleNews", "查看新闻");
        }
        /// <summary>
        /// 添加用户行为
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="userId"></param>
        /// <param name="behaviorName"></param>
        /// <returns></returns>
        public void  AddUserBehavior(int newsId ,int userId, string controllerName)
        {
            string behaviorName = BehaivorControllerMap[controllerName];
            UserBehavior userBehavior = new UserBehavior
            {
                NewsId = newsId,
                UserId = userId,
                BehaviorName = behaviorName,
                OccurTime = DateTime.Now
            };
            _userBehaviorRepository.Insert(userBehavior);
        }
    }
}
