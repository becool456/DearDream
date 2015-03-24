using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;


using DearDreamModels;
using DearDreamModels.ViewModel;
using DearDreamDAL;
using ImplOfRepository;

namespace DearDreamBLL.ImplOfService
{
    public class NewsService:INewsService
    {
        #region 属性

        /// <summary>
        /// 定义新闻仓库类
        /// </summary>
        [Ninject.Inject]
        protected INewsRepository newsRepository { get; set; }

        [Ninject.Inject]
        protected INewsCategoryRepository _newCategoryRepository { get; set; }
        #endregion

        #region 方法

        public IEnumerable<News> GetRandomNews(int num , string category)
        {
            IEnumerable<News> result = new List<News>();
            Random r = new Random();
            IEnumerable<News> container = newsRepository.Entities.Where(o => o.Category == category).OrderBy(o => o.Time).ToList();
            int count = container.Count();
            for(int i = 0 ; i < num ; i++)
            {
                int tmp = r.Next(count);
                var item = container.Skip(tmp - 1).Take(1);
                result = result.Union(item);
            }
            return result;
        }

        /// <summary>
        /// 获取指定类别和数量的新闻概述
        /// </summary>
        /// <param name="category">类别名</param>
        /// <param name="NewsAmount">获取新闻数量</param>
        /// <returns>新闻视图类集合</returns>
        public IEnumerable<News> GetOverNews(string category, int NewsAmount)
        {
            IQueryable<News> newsAsQuery = newsRepository.Entities;
            IEnumerable<News> newsAsEnum = newsRepository.Entities.Where(
                o => o.Category == category).OrderByDescending( n => n.Time).Take(NewsAmount);
            if (newsAsEnum == null)
                throw new Exception("News Value Null!");
            return newsAsEnum;
        }

        /// <summary>
        /// 获取新闻所在的类别
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public string GetNewsType(int ? newsId)
        {
            var news = newsRepository.GetByKey(newsId);
            if (news == null)
                throw new Exception("找不到指定ID的新闻");
            return news.Category;
        }

        /// <summary>
        /// 获取指定数量的总的新闻概述（用于默认首页显示）
        /// </summary>
        /// <param name="NewsAmount"></param>
        /// <returns>新闻总览视图类集合</returns>
        public IEnumerable<News> GetAllNews(int NewsAmount)
        {
            IQueryable<News> newsAsQuery = newsRepository.Entities;
            
            var categoryLst = new List<string>();
            var categoryQry = from d in newsAsQuery
                           orderby d.Category
                           select d.Category;
            categoryLst.AddRange(categoryQry.Distinct());
            
            IEnumerable<News> newsAsEnum = null;
            int i = 1;
            while (i < categoryLst.Count())
            {
                IEnumerable<News> newses = GetOverNews(categoryLst[i], NewsAmount);
                if (i == 1)
                    newsAsEnum = newses;
                else
                    newsAsEnum = Enumerable.Union<News>(newsAsEnum, newses);
                i++;
            }
            return newsAsEnum;            
        }

        /// <summary>
        /// 用于显示单个新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns>新闻视图类集合</returns>
        public News GetSingleNews(int id)
        {
            return newsRepository.Entities.FirstOrDefault(n => n.Id == id);
        }

        /// <summary>
        /// 获取所有新闻类型
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NewsCategory> GetAllNewsType()
        {
            return _newCategoryRepository.Entities.ToList();
        }

        #endregion
    }
}
