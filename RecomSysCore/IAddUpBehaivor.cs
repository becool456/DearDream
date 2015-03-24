using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecomSysCore
{
    public interface IAddUpBehaivor
    {
        /// <summary>
        /// 对指定时间域的用户行为统计建立用户兴趣模型
        /// </summary>
        /// <param name="beginTime">起始时间</param>
        /// <param name="endTime">终点时间</param>
        /// <returns>受影响的用户行为数目</returns>
        int AddUpBehaviorBetween(DateTime beginTime, DateTime endTime );

        /// <summary>
        /// 对指定时间至今的用户行为统计建立用户兴趣模型
        /// </summary>
        /// <param name="beginTime">起始时间</param>
        /// <returns>受影响的用户行为数目</returns>
        int AddUpBehaviorFrom(DateTime beginTime);

        /// <summary>
        /// 对尚未更新的用户新闻统计，更新兴趣模型
        /// </summary>
        /// <returns>受影响的用户行为数目</returns>
        int UpdateBehaviorToLateset();
    }
}
