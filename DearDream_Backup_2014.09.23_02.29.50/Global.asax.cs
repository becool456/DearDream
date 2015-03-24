using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Ninject;
using DearDreamBLL.NinjectHelper;
using DearDreamBLL.ImplOfService;

namespace DearDream
{
    public class MvcApplication : System.Web.HttpApplication
    {
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //配置注册容器NinjectDependencyResolver
            DependencyResolver.SetResolver(new NinjectDependencyResolver());

            //new SqlTreatment().DealNewsNull();
         }
    }
}
