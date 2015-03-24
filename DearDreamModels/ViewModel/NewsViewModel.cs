using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearDreamModels.ViewModel
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        [DisplayName("类别")]
        public string Category { get; set; }
        [DisplayName("标题")]
        public string Title { get; set; }
        [DisplayName("内容")]
        public string Content { get; set; }
        [DisplayName("来源")]
        public string Source { get; set; }
        [DisplayName("发布时间")]
        public DateTime Time { get; set; }
        [DisplayName("关键词")]
        public string Keywords { get; set; }
        [DisplayName("热度")]
        public int Heat { get; set; }
    }
}
