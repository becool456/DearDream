using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DearDreamModels.ViewModel;
using DearDreamModels.Helper;
using DearDreamModels;
using DearDreamBLL;
using Ninject;

namespace DearDream.Controllers
{
    /// <summary>
    /// 用户基本操作控制器
    /// </summary>
    public class AccountController : Controller
    {
        #region 属性
        /// <summary>
        /// 用户服务类
        /// </summary>
        [Ninject.Inject]
        private IAccountService _accountService {get; set;}

        /// <summary>
        /// 缓存服务类
        /// </summary>
        [Ninject.Inject]
        private ICookieService _cookieService { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 用户登陆（HttpGet）
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Authority");
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// 用户登陆（HttpPost）
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost,ActionName("Login")]
        public ActionResult Login(UserLoginViewModel viewModel,string returnUrl)
        {
            bool isLoginSuccess = _accountService.Login(viewModel);
            if(isLoginSuccess)
            { 
                #region 客户端缓存
                _cookieService.SetCookies(Response, Request, "loginName", viewModel.LoginName, "loginInfo", 7);
                _cookieService.SetCookies(Response, Request, "userId", viewModel.UserId.ToString(), "loginInfo", 7);
                _cookieService.SetCookies(Response, Request, "roleName", viewModel.RoleName, "loginInfo", 7);
                #endregion 客户端缓存
                System.Web.Security.FormsAuthentication.SetAuthCookie(viewModel.LoginName, viewModel.IsRememberMe);
                return Redirect(returnUrl);
            }
            TempData["Error"] = "用户名或密码错误";
            ViewBag.returnUrl = returnUrl;
            return View(viewModel);
        }

        /// <summary>
        /// 用户注销(HttpGet)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LogOut()
        {
            string returnUrl = Request.Params["returnUrl"];
            returnUrl = returnUrl ??
                Url.Action("Index", "Authority") ;
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        
        /// <summary>
        /// 用户注销（HttpPost）
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("LogOut")]
        public ActionResult LogOut(string returnUrl)
        {
            if(Request.IsAuthenticated)
            {
                System.Web.Security.FormsAuthentication.SignOut();
                //Session.RemoveAll();
            }
            return Redirect(returnUrl);
        }

        /// <summary>
        /// 用户注册(HttpGet)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

       /// <summary>
       /// 用户注册（HttpPost）
       /// </summary>
       /// <param name="newUser">新用户实体对象</param>
       /// <returns></returns>
        [HttpPost,ActionName("Register")]
        public ActionResult Register(UserRegisterViewModel newUser)
        {
            OperationReuslt registerResult = _accountService.Register(newUser);
            if(!registerResult.isSuccess)
            {
                ModelState.AddModelError("registerError", registerResult.AddInfo);
                return View(newUser);
            }
            return RedirectToAction("Login");
        }
        #endregion 

        /// <summary>
        /// 修改密码（HttpGet）
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePassword()
        {
            return View();
        }

        /// <summary>
        /// 修改密码(HttpPost)
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="newPasswordConfirmed"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePassword(string oldPassword ,string newPassword,string newPasswordConfirmed)
        {
            if(newPassword != newPasswordConfirmed)
            {
                TempData["error"] = "两次密码不一致";
                return View();
            }
            string userName = _cookieService.GetCookies(Request,"loginInfo","loginName");
            OperationReuslt result = _accountService.CheckPassword(userName, oldPassword);
            if(result.isSuccess)
            {
                _accountService.UpdatePassword(userName, newPassword);
                return RedirectToAction("UpdatePasswordSucceed"); 
            }
            TempData["error"] = result.AddInfo;
            return View();
        }

        /// <summary>
        /// 密码修改成功
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePasswordSucceed()
        {
            return View();
        }

        /// <summary>
        /// 显示用户个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowUserInfo()
        {
            int userId = Convert.ToInt32(_cookieService.GetCookies(Request,"loginInfo","userId"));
            UserDetail userDetail = _accountService.GetUserInfo(userId);
            return View(userDetail);
        }

        public ActionResult UpdateUserInfo(int id)
        {
            UserDetail userDetail = _accountService.GetUserInfo(id);
            return View(userDetail);
        }

        [HttpPost]
        public ActionResult UpdateUserInfo(UserDetail userDetail)
        {
            OperationReuslt result =_accountService.UpdateUserInfo(userDetail);
            if (result.isSuccess)
                return RedirectToAction("ShowUserInfo");
            else
            {
                ModelState.AddModelError("error", result.AddInfo);
                return View(userDetail);
            }
        }
    }
}
