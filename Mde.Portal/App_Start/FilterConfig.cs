using Mde.Portal.App_Start;
using System.Web;
using System.Web.Mvc;

namespace Mde.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new JsonAttribute());
            filters.Add(new LocalizationAttribute());
            filters.Add(new CustomerHandleErrorAttribute());
        }
    }
}