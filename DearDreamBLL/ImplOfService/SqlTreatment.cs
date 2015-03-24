using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using Ninject;
using DearDreamDAL;
using DearDreamModels;
using ImplOfRepository;

namespace DearDreamBLL.ImplOfService
{
    /// <summary>
    /// 数据库信息预处理接口
    /// </summary>
    public class SqlTreatment:ISqlTreament
    {
        #region 属性
        [Ninject.Inject]
        public INewsRepository _newsRepository { get; set; }
        #endregion
        /// <summary>
        /// 处理数据库中新闻为空处理
        /// </summary>
        /// <returns></returns>
        public int DealNewsNull()
        {
            Expression<Func<News,bool>> predicate = n =>n.Title == string.Empty||n.Content == string.Empty;
            return _newsRepository.Delete(predicate);
        }


        /// <summary>
        /// 处理新闻摘要为空的情况
        /// </summary>
        /// <returns></returns>
        public int DealNewsDescriptionNull(int length = 50)
        {
            IQueryable<News> newsQuery = _newsRepository.Entities
                .Where(n => n.Description == string.Empty);
            foreach(var item in newsQuery)
            {
                item.Description = item.Title + "...";
                _newsRepository.Update(item, false);
            }
            return _newsRepository.SaveChanges();        
        }

    }
}
