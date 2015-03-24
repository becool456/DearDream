using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ninject;
using DearDreamBLL;
using DearDreamModels;
using DearDreamModels.ViewModel;
using RecomSysCore;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

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

        #endregion

        #region 方法
        public ActionResult Index()
        {
            return View(_newsService.GetOverNews("财经",50));
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

        public ActionResult Category(string Category)
        {
            ViewBag.Title = Category;
            return View(_newsService.GetOverNews(Category, 50));
        }

        [HttpGet]
        public ActionResult SingleNews(int id)
        {
            //获取用户访问新闻行为
            string strUserId = _cookieService.GetCookies(Request,"loginInfo","userId");
            if(strUserId != string.Empty)
            {
                _acquireBehavior.AddUserBehavior(id,Convert.ToInt32(strUserId),"SingleNews");
            }
            return View(_newsService.GetSingleNews(id));
        }
        #endregion
    }


}