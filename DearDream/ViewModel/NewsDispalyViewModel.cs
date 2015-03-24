using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DearDreamModels;

namespace DearDream.ViewModel
{
    public class NewsDispalyViewModel
    {
        //常规新闻
        public IEnumerable<News> CommonNews { get; set; }
        //推荐新闻
        public IEnumerable<News> RecomNews { get; set; }
    }
}