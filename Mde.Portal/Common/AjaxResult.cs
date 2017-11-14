using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mde.Portal.Common
{
    public class AjaxResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object ReturnJson { get; set; } 

    }
}