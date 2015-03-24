using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearDreamBLL
{
    /// <summary>
    /// 数据库信息预处理接口
    /// </summary>
    public interface ISqlTreament
    {
        /// <summary>
        /// 处理数据库中新闻为空处理
        /// </summary>
        /// <returns>受影响的行数</returns>
        int DealNewsNull();

        /// <summary>
        /// 处理新闻摘要为空的情况
        /// </summary>
        /// <returns></returns>
        int DealNewsDescriptionNull(int length = 50);
    }
}
