using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DearDreamModels.ViewModel;
using DearDreamModels;

namespace DearDreamBLL
{
    /// <summary>
    /// 新闻服务接口
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// 获取指定类别和数量的新闻概述
        /// </summary>
        /// <param name="category">类别名</param>
        /// <param name="NewsAmount">获取新闻数量</param>
        /// <returns>新闻类集合</returns>
        IEnumerable<News> GetOverNews(string category, int NewsAmount);

        IEnumerable<News> GetAllNews(int NewsAmount);

        News GetSingleNews(int id);

        /// <summary>
        /// 获取新闻所在的类别
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        string GetNewsType(int? newsId);

        /// <summary>
        /// 获取所有新闻类型
        /// </summary>
        /// <returns></returns>
        IEnumerable<NewsCategory> GetAllNewsType();
        
        /// <summary>
        /// （临时）获取指定类别和数量的随机新闻
        /// </summary>
        /// <param name="num"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        IEnumerable<News> GetRandomNews(int num, string category);
    }
}
