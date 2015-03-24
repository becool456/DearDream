using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace DearDreamBLL
{
    /// <summary>
    /// 缓存服务接口
    /// </summary>
    public interface ICookieService
    {
        /// <summary>  
        /// 设置cookie  
        /// </summary>  
        /// <param name="key">键</param>  
        /// <param name="value">值</param>  
        /// <param name="cookiename">cookie名称</param>  
        /// <param name="cookiedays">cookie保留多少天(默认3天)，可以用小数表示</param>
        void SetCookies(HttpResponseBase Response, HttpRequestBase Request, string key, string value, string cookiename, double cookiedays = 3);
        
        /// <summary>  
        /// 删除cookie，我们没有直接删除的权限，但是新建一个同名cookie,设置为过期，那么浏览器会去检测和删除同名的cookie。  
        /// </summary>  
        /// <param name="cookiename">cookie名称</param>  
        void deleteCookie(HttpResponseBase Response, string cookiename);
        
        /// <summary>  
        /// 获取整个cookie  
        /// </summary>  
        /// <param name="cookiename"></param>  
        /// <returns></returns>  
        HttpCookie GetCookies(HttpRequestBase Request, string cookiename);

        /// <summary>  
        /// 获取cookie里的键对应的值  
        /// </summary>  
        /// <param name="name">名称</param>  
        /// <param name="key">键</param>  
        /// <returns>返回键对应的值</returns> 
        string GetCookie(HttpRequestBase Request, string name, string key);
    }
}
