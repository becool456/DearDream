using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DearDream.ViewModel
{
    public class UserInfoViewModel
    {
        public int Id { get; set; }
        [DisplayName("用户Id")]
        public int UserId { get; set; }
        [DisplayName("昵称")]
        public string NickName { get; set; }
        [DisplayName("邮箱")]
        public string Email { get; set; }
        [DisplayName("头像")]
        public string AvatarName { get; set; }
        [DisplayName("性别")]
        public bool Gender { get; set; }
        [DisplayName("电话")]
        public string Telephone { get; set; }
        [DisplayName("出生日期")]
        public DateTime BirthTime { get; set; }

        [DisplayName("年龄")]
        public int Age { get; set; }
        
        [DisplayName("教育水平")]
        public string EducationlevelName { get; set; }

        [DisplayName("婚姻状况")]
        public string MaritalstatusName { get; set; }

        [DisplayName("性别")]
        public string GenderName { get; set; }

        [DisplayName("行业类别")]
        public string IndustryCategoryName { get; set; }

        [DisplayName("省份")]
        public string PronviceName { get; set; }

        [DisplayName("市区")]
        public string LocationName { get; set; }

    }
}