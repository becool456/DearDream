using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DearDreamBLL;


namespace DearDream.Controllers
{
    public class DisplayPartialController : Controller
    {
        /// <summary>
        /// 缓存服务类
        /// </summary>
        [Ninject.Inject]
        private ICookieService _cookieService { get; set; }

        [Ninject.Inject]
        private INewsService _newsService { get; set; }
        
        /// <summary>
        /// 登陆标题切换
        /// </summary>
        /// <returns></returns>
        public ActionResult HeadSection()
        {
            if (User.Identity.Name != string.Empty)
            {
                ViewBag.LoginName = User.Identity.Name;
                return PartialView("_PartialLoginSuccess");
            }
            else           
                return PartialView("_PartialUnLogin");
        }

        public ActionResult NewsTypes()
        {
            ViewBag.NewsTypes = _newsService.GetAllNewsType();
            return PartialView("_PartialNewsTypes");
        }
    }
}