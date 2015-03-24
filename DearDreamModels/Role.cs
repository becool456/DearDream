using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DearDreamModels
{
    public class Role : Entity
    {
        public int Id { get; set; }
        [DisplayName("角色名")]
        public string RoleName { get; set; }
        [DisplayName("角色状态")]
        public bool RoleState { get; set; }
    }
}
