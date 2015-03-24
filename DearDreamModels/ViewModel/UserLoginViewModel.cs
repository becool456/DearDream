using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DearDreamModels.ViewModel
{
    public class UserLoginViewModel
    {
        [DisplayName("用户Id")]
        public int UserId { get; set; }

        [DisplayName("登陆名")]
        public string LoginName { get; set; }

        [DisplayName("密码")]
        public string PassWord { get; set; }

        [DisplayName("记住我")]
        public bool IsRememberMe { get; set; }

        [DisplayName("返回链接")]
        public string ReturnUrl { get; set; }

        [DisplayName("角色名")]
        public string RoleName { get; set; }
    }
}
