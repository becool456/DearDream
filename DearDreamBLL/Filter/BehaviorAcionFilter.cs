using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using ImplOfRepository;
using RecomSysCore;
using Ninject;

namespace DearDreamBLL.Filter
{
    public class BehaviorAcionFilter:ActionFilterAttribute,IActionFilter
    {
        [Ninject.Inject]
        private IUserRepository _userRepository { get; set; } 

        [Ninject.Inject]
        private IAcquireBehavior _acquireBehavior { get; set; }

        [Ninject.Inject]
        private ICookieService _cookieService { get; set; }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            string name = filterContext.HttpContext.User.Identity.Name;
            if(name != "")
            {
                //int userId = _userRepository.Entities.First(u => u.LoginName == name).Id;
                string tmp = filterContext.RouteData.Route.ToString();
            } 
            base.OnResultExecuted(filterContext);
        }
    }
}
