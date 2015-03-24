using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DearDreamModels
{
    public class Authority:Entity
    {
        public int Id { get; set; }
        [DisplayName("控制器名称")]
        public string ControllerName { get; set; }
        [DisplayName("动作名称")]
        public string ActionName { get; set; }
        [DisplayName("事件名称")]
        public string EventName { get; set; }
    }
}
