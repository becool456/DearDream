using System.Web;
using System.Web.Mvc;
using DearDreamBLL.Filter;

namespace DearDream
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new BehaviorAcionFilter());
        }
    }
}
