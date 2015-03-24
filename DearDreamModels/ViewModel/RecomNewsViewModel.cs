using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DearDreamModels.ViewModel
{
    public class RecomNewsViewModel
    {
        public int Id { get; set; }
        [DisplayName("类别")]
        public string Category { get; set; }
        [DisplayName("标题")]
        public string Title { get; set; }
        [DisplayName("发布时间")]
        public DateTime Time { get; set; }
        [DisplayName("描述")]
        public string Description { get; set; }
        [DisplayName("热度")]
        public int Heat { get; set; }
    }
}
