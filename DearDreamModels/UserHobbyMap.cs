using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearDreamModels
{
    /// <summary>
    /// 用户爱好映射类
    /// </summary>
    public class UserHobbyMap :　Entity
    {
        public int Id { get; set; }

        [DisplayName("用户Id")]
        public int UserId { get; set; }

        [DisplayName("类别Id")]
        public int CategoryId { get; set; }
    }
}
