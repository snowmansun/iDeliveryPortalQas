using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewTruckCheckItemData
    {
        public int Id { get; set; }
        public int CheckListId { get; set; }
        public string Content { get; set; }
        public int ParentId { get; set; }
        public int InputType { get; set; }
        public int InputTypeDesc { get; set; }
        public List<object> children { get; set; }
        
        public ViewTruckCheckItemData()
        {
            children = new List<object>();
        }

    }
}
