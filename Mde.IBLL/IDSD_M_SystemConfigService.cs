using Mde.Model.DataModel;
using Mde.Model.ModelView;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.IBLL
{
    public partial interface IDSD_M_SystemConfigService : IBaseService<DSD_M_SystemConfig>
    {
        List<ViewConfigData> GetConfigData(string category, string keyname, int orgId);

        List<ViewComboboxStringData> GetCategoryComboxData();

        List<ViewComboboxStringData> GetKeyNameComboxData(string category);

        List<ViewSystemConfigData> GetSystemConfigData(string category, string keyName);

        void SaveConfigData(int SystemConfigID, string Category, string KeyName, string Description, string KeyValue, string OrgIDs, string OldOrgIDs, string UserCode);
    }
}
