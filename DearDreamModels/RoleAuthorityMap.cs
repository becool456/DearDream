using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DearDreamModels
{
    public class RoleAuthorityMap : Entity
    {
        public int Id { get; set; }
        [DisplayName("角色")]
        public int RoleId { get; set; }
        [DisplayName("权限")]
        public int AuthorityId { get; set; }
    }
}
