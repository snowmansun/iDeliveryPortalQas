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
    public partial interface IDSD_M_TruckCheckListService:IBaseService<DSD_M_TruckCheckList>
    {
        List<ViewTruckCheckListData> GetTruckCheckList(string Content, string TruckType, ParameterQuery param);

        List<ViewTruckTypeData> GetTruckTypeList(int ListId, ParameterQuery param);

        void SaveTruckAssign(List<DSD_M_TruckCheckAssign> items, int ListId);

        void SaveTruckCheckList(TruckCheckListData item, string LoginName, string mode);

        TruckCheckListData GetTruckCheckDataById(int id);

        DSD_M_TruckCheckList GetDataById(int Id);

        void UpdateTruckList(DSD_M_TruckCheckList item);
    }
}
