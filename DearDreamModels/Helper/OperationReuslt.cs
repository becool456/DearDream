using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DearDreamModels.Helper
{
    public class OperationResult
    {
        #region 属性
        [DisplayName("操作结果")]
        public bool isSuccess { get; set; }

        [DisplayName("附加信息")]
        public string AddInfo { get; set; }
        #endregion 

        #region 方法

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public OperationResult() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="result">操作结果</param>
        public OperationResult(bool result)
        {
            isSuccess = result;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="result">操作结果</param>
        /// <param name="addInfo">附加信息</param>
        public OperationResult(bool result , string addInfo)
            :this(result)
        {
            AddInfo = addInfo;
        }
        #endregion
    }
}
