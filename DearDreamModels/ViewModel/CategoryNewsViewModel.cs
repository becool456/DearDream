using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Web.Mvc;

namespace DearDreamModels.ViewModel
{
    public class CategoryNewsViewModel
    {
        [DisplayName("单类别新闻")]
        public IEnumerable<NewsOverViewModel> PreViewList { get; set; }
        [DisplayName("推荐列表")]
        public IEnumerable<RecomNewsViewModel> RecomList { get; set; }

        [DisplayName("新闻类别名")]
        public string Category { get; set; }
    }
}
