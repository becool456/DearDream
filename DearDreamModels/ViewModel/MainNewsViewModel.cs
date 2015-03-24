using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DearDreamModels.ViewModel
{
    public class MainNewsViewModel
    {
        [DisplayName("最热新闻")]
        public IEnumerable<NewsOverViewModel> PreViewList { get; set; }
        [DisplayName("推荐新闻")]
        public IEnumerable<RecomNewsViewModel> RecomList { get; set; }

        [DisplayName("包含类别")]
        public IEnumerable<NewsCategory> Categories { get; set; }
    }
}
