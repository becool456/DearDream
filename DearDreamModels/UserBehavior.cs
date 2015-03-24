using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DearDreamModels
{
    public class UserBehavior : Entity
    {
        public int Id { get; set; }

        [DisplayName("绑定的用户")]
        public int UserId { get; set; }

        [DisplayName("绑定的新闻")]
        public int NewsId { get; set; }

        [DisplayName("行为名称")]
        public string BehaviorName { get; set; }

        [DisplayName("行为时间")]
        public DateTime OccurTime { get; set; }
        
    }
}
