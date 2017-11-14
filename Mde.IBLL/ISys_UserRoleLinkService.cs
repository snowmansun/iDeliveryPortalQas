using Mde.Model.DataModel;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.IBLL
{
    public partial interface ISys_UserRoleLinkService : IBaseService<Sys_UserRoleLink>
    {
        List<Sys_UserRoleLink> GetDataByUserCode(string UserCode);

        Sys_UserRoleLink GetData(string UserCode, int RoleID);

        void AddUserRoleLink(Sys_UserRoleLink item);

        void UpdateUserRoleLink(Sys_UserRoleLink item);
    }
}
