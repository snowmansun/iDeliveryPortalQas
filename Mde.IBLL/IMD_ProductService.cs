using Mde.Common;
using Mde.Model.DataModel;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.IBLL
{
    public partial interface IMD_ProductService:IBaseService<MD_Product>
    {
        List<ViewProductData> GetProductList(string Product, string Brand, string Pack, string Status, bool IsEmpty, bool Equipment, ParameterQuery param);
    }
}
