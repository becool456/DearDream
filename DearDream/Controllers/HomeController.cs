using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Ninject;
using DearDream.ViewModel;
using DearDreamBLL;
using DearDreamModels;
using DearDreamModels.ViewModel;
using RecomSysCore;
using RecomSysCore.ImplOfRecom;


namespace DearDream.Controllers
{
    public class HomeController : Controller
    {
        #region 属性
        /// <summary>
        /// 新闻服务接口
        /// </summary>
        [Ninject.Inject]
        private INewsService _newsService { get; set; }

        /// <summary>
        /// 用户行为接口
        /// </summary>
        [Ninject.Inject]
        private IAcquireBehavior _acquireBehavior { get; set; }

        /// <summary>
        /// Cookie操作接口
        /// </summary>
        [Ninject.Inject]
        private ICookieService _cookieService { get; set; }

        /// <summary>
        /// 新闻推荐服务
        /// </summary>
        [Inject]
        private IAcquireRecom _recomService { get; set; }
        #endregion

        #region 方法
        public ActionResult Index()
        {
            MainNewsViewModel newsContainer = new MainNewsViewModel();
            IList<NewsCategory> categories = new List<NewsCategory>();
            categories.Add(new NewsCategory { Name = "娱乐" });
            categories.Add(new NewsCategory { Name = "星座" });
            categories.Add(new NewsCategory { Name = "财经" });
            IEnumerable<News> preNews = new List<News>();
            foreach(var item in categories)
            {
                preNews = preNews.Union(_newsService.GetOverNews(item.Name, 15));
            }
            newsContainer.Categories = categories;
            Mapper.CreateMap<News, NewsOverViewModel>();
            newsContainer.PreViewList = Mapper.Map<IEnumerable<News>, IEnumerable<NewsOverViewModel>>(preNews);
            IEnumerable<News> tmpRecomList;
            if (Request.IsAuthenticated)
                tmpRecomList = _recomService.AcquireTotalRecom(true, Convert.ToInt32(_cookieService.GetCookie(Request, "loginInfo", "userId")), 30);
            else
                tmpRecomList = _recomService.AcquireTotalRecom(false, 0, 20);
            Mapper.CreateMap<News, RecomNewsViewModel>();
            newsContainer.RecomList = Mapper.Map<IEnumerable<News>, IEnumerable<RecomNewsViewModel>>(tmpRecomList);
            return View(newsContainer);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Category(string category)
        {
            if (category == null)
                throw new Exception("category参数为空！");
            CategoryNewsViewModel viewModel = new CategoryNewsViewModel();
            viewModel.Category = category + "新闻";
            var preNews = _newsService.GetOverNews(category, 30);
            Mapper.CreateMap<News, NewsOverViewModel>();
            viewModel.PreViewList = Mapper.Map<IEnumerable<News>, IEnumerable<NewsOverViewModel>>(preNews);
            string strUserId = _cookieService.GetCookie(Request, "loginInfo", "userId");
            IEnumerable<News> tmpRecomList = null;
            if (strUserId != string.Empty)
                tmpRecomList = _recomService.AcquireSingleTypeRecom(category, 20, Convert.ToInt32(strUserId));
            else
                tmpRecomList = _recomService.AcquireSingleTypeRecom(category, 20);
            Mapper.CreateMap<News, RecomNewsViewModel>();
            viewModel.RecomList = Mapper.Map<IEnumerable<News>, IEnumerable<RecomNewsViewModel>>(tmpRecomList);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult SingleNews(int id)
        {
            //获取用户访问新闻行为
            string strUserId = _cookieService.GetCookie(Request,"loginInfo","userId");
            if(strUserId != string.Empty)
            {
                _acquireBehavior.AddUserBehavior(id,Convert.ToInt32(strUserId),"SingleNews");
            }

            #region 获取相关新闻列表
            SingleNewsViewModel result = new SingleNewsViewModel();
            result.News = _newsService.GetSingleNews(id);
            IEnumerable<News> tmpRecomList = null;
            tmpRecomList = _recomService.AcquireRelatedNews(id);
            Mapper.CreateMap<News, RecomNewsViewModel>();
            result.RelatedList = Mapper.Map<IEnumerable<News>, IEnumerable<RecomNewsViewModel>>(tmpRecomList);
            #endregion

            #region 获取看过该新闻的用户看过新闻
            if(!string.IsNullOrEmpty(strUserId))
            {
                string newsType = _newsService.GetNewsType(id);
                var seenRelatedList = _newsService.GetRandomNews(10, newsType);
                result.SeenRelatedList = Mapper.Map<IEnumerable<News>, IEnumerable<RecomNewsViewModel>>(seenRelatedList);
            }
            #endregion
            return View(result);
        }
        #endregion
    }
}