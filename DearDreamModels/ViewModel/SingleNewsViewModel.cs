using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace DearDreamModels.ViewModel
{
    public class SingleNewsViewModel
    {
        [DisplayName("当前查看新闻")]
        public News News { get; set; }
        [DisplayName("相关新闻")]
        public IEnumerable<RecomNewsViewModel> RelatedList { get; set; }
        [DisplayName("看过此条新闻还看过")]
        public IEnumerable<RecomNewsViewModel> SeenRelatedList { get; set; }
    }
}
