using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DearDreamModels.ViewModel
{
    public class UserRegisterViewModel
    {
        [DisplayName("登陆名")]
        public string LoginName { get; set; }
        [DisplayName("密码")]
        public string PassWord { get; set; }
        [DisplayName("确认密码")]
        public string PassWordConfirmed { get; set; }
        [DisplayName("邮箱")]
        public string Email { get; set; }
    }
}
