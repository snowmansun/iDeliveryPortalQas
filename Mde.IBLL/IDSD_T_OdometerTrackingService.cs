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
    public partial interface IDSD_T_OdometerTrackingService:IBaseService<DSD_T_OdometerTracking>
    {
        List<ViewOdometerTrackingData> LoadOdometerData(string Truck, string DateFrom, string DateTo, string Status,ParameterQuery param);
    }
}
