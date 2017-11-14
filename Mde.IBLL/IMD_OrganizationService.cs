using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mde.Model.DataModel;
using Mde.Model.ModelView;
using Mde.ModelView.ModelView;

namespace Mde.IBLL
{
    public partial interface IMDOrganizationService : IBaseService<MD_Organization>
    {
        List<ViewComboboxIntData> GetComboboxData();

        List<object> GetOrganizationTreeData(int parentId, bool IsAll);

        List<object> GetOrganizationTreeData(int parentId, string orgIDs);

        List<ViewTreeGridData> LoadOrganizationData();

        MD_Organization CheckData(string Code);

        MD_Organization GetDataById(int Id);

        int GetLevelById(int Id);
    }
}
