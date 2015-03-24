using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace DearDreamModels
{
    public class UserDetail : Entity
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
        [DisplayName("电话")]
        public string Telephone { get; set; }
    }
}
