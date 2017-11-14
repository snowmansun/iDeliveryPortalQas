using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;

namespace Mde.Portal
{
    public class LocalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cookie = filterContext.HttpContext.Request.Cookies["CurrentUICulture"];
            if (cookie == null)
            {
                /// 把设置保存进cookie
                try
                {
                    HttpCookie _cookie = new HttpCookie("CurrentUICulture", Thread.CurrentThread.CurrentUICulture.Name);
                    _cookie.Expires = DateTime.Now.AddDays(1);
                    filterContext.HttpContext.Response.SetCookie(_cookie);
                }
                catch
                { }
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cookie.Value);
            }

            base.OnActionExecuting(filterContext);

        }
    }
}