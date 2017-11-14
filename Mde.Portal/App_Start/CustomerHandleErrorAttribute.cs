using Mde.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mde.Portal.App_Start
{
    public class CustomerHandleErrorAttribute : HandleErrorAttribute 
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //base.OnException(filterContext);

            //if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }
            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                //AJAX方法
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            //记录错误日志
            Log.Error(filterContext.Exception);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

        }
    }
}