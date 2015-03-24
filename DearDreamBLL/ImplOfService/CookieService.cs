using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace DearDreamBLL.ImplOfService
{
    /// <summary>
    /// 缓存服务实现
    /// </summary>
    public class CookieService:ICookieService
    {
        #region 方法
        /// <summary>  
        /// 设置cookie  
        /// </summary>  
        /// <param name="key">键</param>  
        /// <param name="value">值</param>  
        /// <param name="cookiename">cookie名称</param>  
        /// <param name="cookiedays">cookie保留多少天(默认3天)，可以用小数表示</param>
        public void SetCookies(HttpResponseBase Response, HttpRequestBase Request, string key, string value, string cookiename, double cookiedays = 3)
        {
            HttpCookie cookie = Request.Cookies[cookiename];
            //cookie.ExpirationSet = false;  
            if (cookie == null)
            {
                cookie = new HttpCookie(cookiename);

                cookie.Values.Add(key, value);
            }
            else
            {
                if (cookie.Values[key] != null)
                {
                    cookie[key] = value;
                }
                else cookie.Values.Add(key, value);
            }
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            cookie.Expires = DateTime.Now.AddDays(cookiedays);
            Response.Cookies.Add(cookie);
        }

        /// <summary>  
        /// 删除cookie，我们没有直接删除的权限，但是新建一个同名cookie,设置为过期，那么浏览器会去检测和删除同名的cookie。  
        /// </summary>  
        /// <param name="cookiename">cookie名称</param>  
        public void deleteCookie(HttpResponseBase Response, string cookiename)
        {
            if (Response.Cookies[cookiename].HasKeys)
            {
                HttpCookie aCookie = new HttpCookie(cookiename);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(aCookie);
            }
        }

        /// <summary>  
        /// 获取整个cookie  
        /// </summary>  
        /// <param name="cookiename"></param>  
        /// <returns></returns>  
        public HttpCookie GetCookies(HttpRequestBase Request, string cookiename)
        {
            return HttpContext.Current.Request.Cookies[cookiename];
        }

        /// <summary>  
        /// 获取cookie里的键对应的值  
        /// </summary>  
        /// <param name="name">名称</param>  
        /// <param name="key">键</param>  
        /// <returns>返回键对应的值</returns> 
        public string GetCookie(HttpRequestBase Request, string name, string key)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(key))
            {
                return String.Empty;
            }
            else
            {
                HttpCookie cookie = Request.Cookies[name];
                if (cookie != null && cookie[key] != null)
                {
                    string value = cookie[key].ToString();
                    return HttpContext.Current.Server.UrlDecode(value);

                }
                else
                {
                    return String.Empty;
                }
            }
        }
        #endregion
    }
}
