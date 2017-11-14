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
    public partial interface IMD_UserService : IBaseService<MD_User>
    {
        MD_User LoadUserInfo(string userCode, string password);

        MD_Person LoadPerson(string userCode);

        List<ViewUserData> LoadUserList(string orgId, string userCode, string type, string roleId, string status, ParameterQuery param);

        List<ViewUserData> LoadUserForRole(int roleId, string UserCode, ParameterQuery param);

        MD_User CheckData(string UserCode);

        MD_Person GetPersonByCode(string UserCode);

        void AddUser(MD_User user, MD_Person person, List<Sys_UserRoleLink> userRoleListNew);

        void ModifiedUser(MD_User user, MD_Person person, List<Sys_UserRoleLink> userRoleListNew, List<Sys_UserRoleLink> userRoleListOld);

        OperationResult DeleteUser(MD_User data);

        List<ViewComboboxStringData> GetPersonList(string UserType, bool NeedSum);

        List<ViewComboboxStringData> GetDriverList();
    }
}
