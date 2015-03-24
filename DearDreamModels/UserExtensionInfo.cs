using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DearDreamModels.Helper;

namespace DearDreamModels
{
    /// <summary>
    /// 用户扩展信息类
    /// </summary>
    public class UserExtensionInfo : Entity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        
        #region 数值型
        [DisplayName("出生日期")]
        public DateTime BirthTime { get; set; }

        [DisplayName("年龄")]
        public int Age { get; set; }

        [DisplayName("身高")]
        public int Height { get; set; }
        #endregion

        #region 名称型
        [DisplayName("教育水平编码")]
        public string EducationlevelCode { get; set; }
        
        [DisplayName("教育水平")]
        public string EducationlevelName { get; set; }

        [DisplayName("婚姻状况编码")]
        public string MaritalstatusCode { get; set; }

        [DisplayName("婚姻状况")]
        public string MaritalstatusName { get; set; }

        [DisplayName("性别编码")]
        public string GenderCode { get; set; }

        [DisplayName("性别")]
        public string GenderName { get; set; }

        [DisplayName("行业类别编码")]
        public string IndustryCategoryCode { get; set; }

        [DisplayName("行业类别")]
        public string IndustryCategoryName { get; set; }

        [DisplayName("省份编码")]
        public string PronviceCode { get; set; }

        [DisplayName("省份")]
        public string PronviceName { get; set; }
        
        [DisplayName("市区编码")]
        public string LocationCode { get; set; }

        [DisplayName("市区")]
        public string LocationName { get; set; }

        #endregion

        [DisplayName("名称型属性编码")]
        public string SumCode { get; set; }
    }
}
