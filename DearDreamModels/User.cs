using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DearDreamModels
{
    public class User : Entity
    {
        public int Id { get; set; }
        [DisplayName("登陆名")]
        public string LoginName { get; set; }
        [DisplayName("密码")]
        public string PassWord { get; set; }
        [DisplayName("角色ID")]
        public int RoleId { get; set; }
        [DisplayName("角色名")]
        public string RoleName { get; set; }
        [DisplayName("详细信息")]
        public int DetailId { get; set; }
        [DisplayName("创建时间")]
        public DateTime CreateAt { get; set; }
    }
}
