using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecomSysCore
{
    /// <summary>
    /// 用户行为获取接口
    /// </summary>
    public interface IAcquireBehavior
    {
        /// <summary>
        /// 添加用户行为
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="userId"></param>
        /// <param name="behaviorName"></param>
        void AddUserBehavior(int newsId, int userId, string behaviorName);
    }
}
