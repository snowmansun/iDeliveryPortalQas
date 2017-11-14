using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mde.Portal.Model
{
    public class NavigationMenuModel
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public string menu_id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 页面地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// icon名称
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<object> children { get; set; }

        public NavigationMenuModel()
        {
            children = new List<object>();
        }
    }
}