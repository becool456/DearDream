using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DearDreamModels;
using DearDreamModels.ViewModel;
using DearDreamModels.Helper;
using ImplOfRepository;
using Ninject;
using AutoMapper;


namespace RecomSysCore.ImplOfRecom
{
    public class AcquireRecom : IAcquireRecom
    {
        #region 成员
        [Ninject.Inject]
        public IUserBehaviorRepository _uesrBehaviroDB { get; set; }
        [Ninject.Inject]
        public IUserInterestRepository _userInterestDB { get; set; }
        [Ninject.Inject]
        public INewsRepository _newsDB { get; set; }
        [Ninject.Inject]
        public INewsCategoryRepository _newsCategoryDB { get; set; }
        [Inject]
        public INewsSimilarityMapRepository _newsSimilarityDB { get; set; }
        #endregion 

        #region 接口实现
        /// <summary>
        /// 获取指定数目的新闻推荐结果
        /// </summary>
        /// <param name="isLogin"></param>
        /// <param name="userId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public IEnumerable<News> AcquireTotalRecom(bool isLogin = false, int userId = 0, int num = 100)
        {
            IEnumerable<News> recomResult;
            if (isLogin)
            {
                recomResult =  AcquireRecomByUserBehavior(userId, num);
            }
            else
            {
                recomResult = AcquireRecomByAnonymous(num);
            }
            //Mapper.CreateMap<News, RecomNewsViewModel>();
            //result = Mapper.Map<IEnumerable<News>, IEnumerable<RecomNewsViewModel>>(recomResult);
            return recomResult; 
        }

        /// <summary>
        /// 获取指定类型和指定数目新闻推荐结果
        /// </summary>
        /// <param name="newsType">推荐新闻类型</param>
        /// <param name="num">推荐新闻数目</param>
        /// <returns></returns>
        public IEnumerable<News> AcquireSingleTypeRecom(string newsType, int num = 100,int ? userId = null)
        {
            IEnumerable<News> newsContainer = _newsDB.Entities.Where(
                o => o.Category == newsType).OrderBy(o => o.Time).Take(3*num);
            newsContainer = GetByRandom(newsContainer, num);
            if(userId != null)
                newsContainer = SeenNewsFilter(newsContainer, userId);
            return newsContainer;
        }

        /// <summary>
        /// 获取与当前新闻相似度最高的N个新闻
        /// </summary>
        /// <param name="newsId">当前新闻ID</param>
        /// <returns></returns>
        public IEnumerable<News> AcquireRelatedNews(int? newsId)
        {
            var newsSimilarityMap = _newsSimilarityDB.Entities.SingleOrDefault(o => o.OwingNewsId == newsId);
            if (newsSimilarityMap == null)
                throw new Exception(string.Format("未找到ID为{0}的新闻相似度映射", newsId));
            var newsIDs = Serialization.ConvertStrToEnum(newsSimilarityMap.StrRelatedNewsIds);
            IList<News> result = new List<News>();
            foreach(var item in newsIDs)
            {
                var news = _newsDB.GetByKey(item);
                result.Add(news);
            }
            return result;
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 根据用户兴趣模型获取新闻推荐结果
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        protected IEnumerable<News> AcquireRecomByUserBehavior(int userId, int num = 100)
        {
            IEnumerable<News> recomResultList = new List<News>();
            IEnumerable<UserInterest> userInterests = _userInterestDB.Entities.Where(
                o => o.UserId == userId);
            //根据兴趣分布取出各个类别的新闻并且合并
            foreach (var type in _newsCategoryDB.Entities)
            {
                var typeInterest = userInterests.Single(o => o.NewsId == type.Id);
                int typeNum = (int)(num * typeInterest.Proportion);
                var typeNewsContainer = AcquireSingleTypeRecom(type.Name, typeNum,userId);
                recomResultList = recomResultList.Union(typeNewsContainer);
            }
            return recomResultList;
        }

        /// <summary>
        /// 匿名用户获取推荐结果
        /// </summary>
        /// <param name="num">新闻推荐数量</param>
        /// <returns></returns>
        protected IEnumerable<News> AcquireRecomByAnonymous(int num)
        {
            var newsCategories = _newsCategoryDB.Entities;
            int singleNum = num / newsCategories.Count();
            IEnumerable<News> result = new List<News>();
            foreach(var item in newsCategories)
            {
                var news = AcquireSingleTypeRecom(item.Name, singleNum);
                result = result.Union(news);
            }
            return result;
        }


        /// <summary>
        /// 过滤新闻集合中用户已阅读过的新闻
        /// </summary>
        /// <param name="newsContainer">新闻集合</param>
        /// <param name="userId">用户ID</param>
        /// <returns>过滤后的结果</returns>
        protected IEnumerable<News> SeenNewsFilter(IEnumerable<News> newsContainer, int ? userId)
        {
            IList<News> filterResultList = new List<News>();
            IQueryable<UserBehavior> oldNewsBehavior = _uesrBehaviroDB.Entities.Where
                (o => o.UserId == userId);
            if (oldNewsBehavior == null)
                return newsContainer;
            foreach (var news in newsContainer)
            {
                if (!oldNewsBehavior.Any(o => o.NewsId == news.Id))
                    filterResultList.Add(news);
            }
            return filterResultList;
        }

        /// <summary>
        /// 从新闻集合中随机抽取多条新闻
        /// </summary>
        /// <param name="newsContainer"></param>
        /// <param name="num">随机抽取的新闻数目</param>
        /// <returns></returns>
        protected IEnumerable<News> GetByRandom(IEnumerable<News> newsContainer, int num)
        {
            Random r = new Random();
            int size = newsContainer.Count();
            IEnumerable<News> result = new List<News>();
            for(int i = 0 ; i < num ; i++)
            {
                int tmp = r.Next(size);
                result = result.Union(newsContainer.Skip(tmp).Take(1));
            }
            return result; 
        }

        #endregion
    }   
}
