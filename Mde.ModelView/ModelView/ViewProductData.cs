using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewProductData
    {
        public string ProductCode { set; get; }
        public string ProductName { set; get; }
        public string Brand { set; get; }
        public string Pack { set; get; }
        public string PackType { set; get; }
        public bool IsEmpty { set; get; }
        public string ProductGroup { set; get; }
        public string Category { set; get; }
        public string BaseUom { set; get; }
        public string Flavor { set; get; }
        public string Package { set; get; }
        public string Description { set; get; }
        public bool Status { set; get; }
        public string CreateBy { set; get; }
        public string LastModifiedBy { set; get; }
    }
}
