using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DearDreamModels;
using DearDreamModels.ViewModel;

namespace RecomSysCore
{
    public interface IAcquireRecom
    {
        /// <summary>
        /// 获取指定数目的新闻推荐结果
        /// </summary>
        /// <param name="isLogin"></param>
        /// <param name="userId"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        IEnumerable<News> AcquireTotalRecom(bool isLogin ,int userId = 0,int num = 100);

        /// <summary>
        /// 获取指定类型和指定数目新闻推荐结果
        /// </summary>
        /// <param name="newsType">推荐新闻类型</param>
        /// <param name="num">推荐新闻数目</param>
        /// <returns></returns>
        IEnumerable<News> AcquireSingleTypeRecom(string newsType, int num = 100 ,int ? userId = null) ;

        /// <summary>
        /// 获取与当前新闻相似度最高的N个新闻
        /// </summary>
        /// <param name="newsId">当前新闻ID</param>
        /// <returns></returns>
        IEnumerable<News> AcquireRelatedNews(int? newsId);
    }
}
