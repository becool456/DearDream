using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DearDreamModels.ViewModel;
using DearDreamModels.Helper;
using DearDreamModels;
using DearDreamBLL;
using Ninject;
using Kendo.Mvc.UI;
using Kendo.Mvc;
using DearDream.ViewModel;

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

        [Ninject.Inject]
        private INewsService _newsService { get; set; }
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
            returnUrl = returnUrl ?? Url.Content("~/Home/Index");
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
                #region 客户端缓
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
                Url.Content("~/Home/Index");
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
            OperationResult registerResult = _accountService.Register(newUser);
            if(!registerResult.isSuccess)
            {
                ModelState.AddModelError("registerError", registerResult.AddInfo);
                return View(newUser);
            }
            return RedirectToAction("Login");
        }
        

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
            string userName = _cookieService.GetCookie(Request,"loginInfo","loginName");
            OperationResult result = _accountService.CheckPassword(userName, oldPassword);
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
        [HttpGet]
        public ActionResult ShowUserInfo()
        {
            UserInfoViewModel viewModel = new UserInfoViewModel();
            int userId = Convert.ToInt32(_cookieService.GetCookie(Request, "loginInfo", "userId"));
            var userDetail = _accountService.GetUserInfo(userId);
            var userExtensionInfo = _accountService.GetUserExtensionInfo(userId);
            //convert userDetail and userExtenDetail to viewModel
            Mapper.CreateMap<UserDetail, UserInfoViewModel>();
            Mapper.CreateMap<UserExtensionInfo, UserInfoViewModel>();
            viewModel = Mapper.Map<UserExtensionInfo, UserInfoViewModel>(userExtensionInfo);
            viewModel = Mapper.Map<UserDetail, UserInfoViewModel>(userDetail);
            //shit~value of BirthTime sent wrong,add the following line
            viewModel.BirthTime = userExtensionInfo.BirthTime;
            //sent the selections
            ViewBag.EducationlevelName = new SelectList(_accountService.GetEducationlevels(), "Name", "Name", userExtensionInfo.EducationlevelName);
            ViewBag.PronviceName = new SelectList(_accountService.GetPrinvice(), "Name", "Name", userExtensionInfo.PronviceName);
            ViewBag.MaritalstatusName = new SelectList(_accountService.GetMaritalstatus(), "Name", "Name", userExtensionInfo.MaritalstatusName);
            ViewBag.IndustryCategoryName = new SelectList(_accountService.GetIndustryCategories(), "Name", "Name", userExtensionInfo.IndustryCategoryName);
            ViewBag.GenderName = new SelectList(_accountService.GetGenders(), "Name", "Name", userExtensionInfo.GenderName);
            //ViewBag.Locations = new SelectList(_accountService.GetChildLocationsByName("未设置"), "Name", "Name");
            return View(viewModel);
        }

        [HttpPost, ActionName("ShowUserInfo")]
        public ActionResult ShowUserInfo(UserInfoViewModel viewModel)
        { 
            if(ModelState.IsValid)
            {
                Mapper.CreateMap<UserInfoViewModel,UserDetail>();
                Mapper.CreateMap<UserInfoViewModel,UserExtensionInfo>();
                UserDetail userDetail = Mapper.Map<UserInfoViewModel, UserDetail>(viewModel);
                UserExtensionInfo userExtensionInfo = Mapper.Map<UserInfoViewModel, UserExtensionInfo>(viewModel);
                try
                {
                    _accountService.UpdateUserInfo(userDetail,false);
                    _accountService.UpdateUserExtensionInfo(userExtensionInfo);
                }
                catch(Exception e)
                {
                    throw e;
                }
                ViewBag.UpdateInfo = "保存成功";
            }
            else
            {
                ViewBag.UpdateInfo = "保存失败";
            }
            //sent the selections
            ViewBag.EducationlevelName = new SelectList(_accountService.GetEducationlevels(), "Name", "Name", viewModel.EducationlevelName);
            ViewBag.PronviceName = new SelectList(_accountService.GetPrinvice(), "Name", "Name", viewModel.PronviceName);
            ViewBag.MaritalstatusName = new SelectList(_accountService.GetMaritalstatus(), "Name", "Name", viewModel.MaritalstatusName);
            ViewBag.IndustryCategoryName = new SelectList(_accountService.GetIndustryCategories(), "Name", "Name", viewModel.IndustryCategoryName);
            ViewBag.GenderName = new SelectList(_accountService.GetGenders(), "Name", "Name", viewModel.GenderName);
            return View(viewModel);
        }

        public ActionResult UpdateUserHobbies(string updateInfo)
        {
            int userId = Convert.ToInt32(_cookieService.GetCookie(Request, "loginInfo", "userId"));
            var userHobbies = _accountService.GetUserHobbyMaps(userId);
            UserHobbyMapViewModel viewModel = new UserHobbyMapViewModel();
            viewModel.Hobbies = userHobbies;
            viewModel.UserId = userId;
            viewModel.HobbySelctions = _newsService.GetAllNewsType();
            ViewBag.UpdateInfo = updateInfo;
            return View(viewModel);
        }
        
        [HttpPost]
        public ActionResult UpdateUserHobbies(int[] Hobbies)
        {
            var userHobbyMaps = new List<UserHobbyMap>();
            int userId = Convert.ToInt32(_cookieService.GetCookie(Request, "loginInfo", "userId"));
            for (int i = 0; i < Hobbies.Count(); i++)
            {
                userHobbyMaps.Add(new UserHobbyMap
                {
                    UserId = userId,
                    CategoryId = Hobbies[i]
                });
            }
            try
            {
                var result = _accountService.UpdateUserHobbyMaps(userHobbyMaps,userId);
                if(result.isSuccess == true)
                {
                    return RedirectToAction("UpdateUserHobbies", new { updateInfo = "更新成功" });
                }  
                else
                {
                    return RedirectToAction("UpdateUserHobbies", new { updateInfo = result.AddInfo });
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /*
        public ActionResult UpdateUserInfo(int id)
        {
            UserDetail userDetail = _accountService.GetUserInfo(id);
            return View(userDetail);
        }

        [HttpPost]
        public ActionResult UpdateUserInfo(UserDetail userDetail)
        {
            OperationResult result =_accountService.UpdateUserInfo(userDetail);
            if (result.isSuccess)
                return RedirectToAction("ShowUserInfo");
            else
            {
                ModelState.AddModelError("error", result.AddInfo);
                return View(userDetail);
            }
        }
        [HttpGet]
        public ActionResult UpdateExtensionInfo(int id)
        {
            var userExtensionInfo = _accountService.GetUserExtensionInfo(id);
            ViewBag.EducationlevelName = new SelectList(_accountService.GetEducationlevels(), "Name", "Name",userExtensionInfo.EducationlevelName);
            ViewBag.PronviceName = new SelectList(_accountService.GetPrinvice(), "Name", "Name", userExtensionInfo.PronviceName);
            ViewBag.MaritalstatusName = new SelectList(_accountService.GetMaritalstatus(), "Name", "Name", userExtensionInfo.MaritalstatusName);
            ViewBag.IndustryCategoryName = new SelectList(_accountService.GetIndustryCategories(), "Name", "Name", userExtensionInfo.IndustryCategoryName);
            ViewBag.GenderName = new SelectList(_accountService.GetGenders(), "Name", "Name", userExtensionInfo.GenderName);
            //ViewBag.Locations = new SelectList(_accountService.GetChildLocationsByName("未设置"), "Name", "Name");
            return View(userExtensionInfo);
        }
        [HttpPost]
        public ActionResult UpdateExtensionInfo(UserExtensionInfo ui)
        {
            if(ModelState.IsValid)
            {
                OperationResult result = _accountService.UpdateUserExtensionInfo(ui);
                ViewBag.UpdateResult = result.AddInfo;
                return RedirectToAction("ShowUserInfo");
            }
            return View(ui);
        }

        #endregion

        #region 辅助方法

        public JsonResult GetEducationlevel()
        {
            return Json(_accountService.GetEducationlevels(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProvinces()
        {
            return Json(_accountService.GetPrinvice(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocations(string parentName)
        {
            return Json(_accountService.GetChildLocationsByName(parentName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMaritalstatus()
        {
            return Json(_accountService.GetMaritalstatus(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIndustryCategories()
        {
            return Json(_accountService.GetIndustryCategories(), JsonRequestBehavior.AllowGet);            
        }
        
        public JsonResult GetGenders()
        {
            return Json(_accountService.GetGenders(), JsonRequestBehavior.AllowGet);
        }
         * */
        #endregion
    }
}
