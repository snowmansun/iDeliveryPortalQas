using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mde.IBLL;
using Mde.Model.DataModel;
using System.Data.Common;
using Mde.ModelView.ModelView;
using System.Transactions;
namespace Mde.BLL
{
    public partial class Sys_UserRoleLinkService : BaseService<Sys_UserRoleLink>, ISys_UserRoleLinkService
    {
        public List<Sys_UserRoleLink> GetDataByUserCode(string UserCode)
        {
            var data = _dbSession.Sys_UserRoleLinkRepository.Entities;
            if (!string.IsNullOrEmpty(UserCode))
            {
                data = data.Where(p => p.UserCode == UserCode);
            }
            return data.ToList();
        }

        public Sys_UserRoleLink GetData(string UserCode, int RoleID)
        {
            Sys_UserRoleLink data = _dbSession.Sys_UserRoleLinkRepository.Entities.Where(p => p.UserCode == UserCode && p.RoleID == RoleID).FirstOrDefault();
            return data;
        }

        public void AddUserRoleLink(Sys_UserRoleLink item)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _dbSession.Sys_UserRoleLinkRepository.AddEntities(item);
                _dbSession.Sys_UserRoleLinkRepository.Commit();

                scope.Complete();
            }
        }

        public void UpdateUserRoleLink(Sys_UserRoleLink item)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _dbSession.Sys_UserRoleLinkRepository.UpdateEntities(item);
                _dbSession.Sys_UserRoleLinkRepository.Commit();

                scope.Complete();
            }

        }
    }
}
