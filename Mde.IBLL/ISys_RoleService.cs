using Mde.Common;
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
    public partial interface ISys_RoleService : IBaseService<Sys_Role>
    {
        List<Sys_Role> LoadSysRoleInfo(string RoleNm);

        List<ViewComboboxIntData> GetComboboxData();

        List<ViewUserRoleData> GetRoleData(ParameterQuery param, string UserCode);

        Sys_Role CheckData(int RoleId);

        void AddData(Sys_Role data);

        void UpdateData(Sys_Role data);
    }
}
