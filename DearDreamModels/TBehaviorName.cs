using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DearDreamModels
{
    public class TBehaviorName:Entity
    {
        public int Id { get; set; }

        [DisplayName("用户行为名称")]
        public string BehaviorName { get; set; }

        [DisplayName("行为权重")]
        public double Weight { get; set; }
    }
}
