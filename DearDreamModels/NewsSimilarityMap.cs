using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DearDreamModels
{
    public class NewsSimilarityMap:Entity
    {
        public int Id { get; set; }
        [DisplayName("映射的新闻ID")]
        public int OwingNewsId { get; set; }
        [DisplayName("相关的新闻ID")]
        public string StrRelatedNewsIds { get; set; }
        [DisplayName("相关度")]
        public string StrSimilarities { get; set; }
        [DisplayName("更新时间")]
        public DateTime UpdateTime { get; set; }
    }
}
