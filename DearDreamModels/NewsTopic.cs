using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DearDreamModels
{
    public class NewsTopic:Entity
    {
        [Key]
        public int TopicID { get; set; }

        public string TopicName { get; set; }

        public DateTime UpdateTime { get; set; }

        public int Heat { get; set; }
    }
}
