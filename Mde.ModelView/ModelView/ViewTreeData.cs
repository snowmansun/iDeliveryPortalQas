using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewTreeData
    {
        public string id { get; set; }

        public string text { get; set; }

        public TreeAttributes attributes { get; set; }

        public string iconCls { get; set; }

        public string state { get; set; }

        public bool _ischecked { get; set; }

        public List<object> children { get; set; }

        public ViewTreeData()
        {
            children = new List<object>();
            attributes = new TreeAttributes();
            iconCls = "icon-none";
        }
    }

    public class TreeAttributes
    {
        public string parentid { get; set; }
        public string level { get; set; }
        public string fullpath { get; set; }
        public string isValid { get; set; }
    }
}
