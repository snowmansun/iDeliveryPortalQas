using Mde.Common;
using Mde.IBLL;
using Mde.Model.DataModel;
using Mde.Model.ModelView;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Mde.BLL
{
    public partial class MD_UserService : BaseService<MD_User>, IMD_UserService
    {
        /// <summary>
        /// 验证帐号密码
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public MD_User LoadUserInfo(string userCode,string password)
        {
            MD_User data;

            data = _dbSession.MD_UserRepository.Entities.Where(x => x.UserCode == userCode && x.Password == password).FirstOrDefault();

            return data;
        }

        /// <summary>
        /// load user list
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="userCode"></param>
        /// <param name="type"></param>
        /// <param name="roleId"></param>
        /// <param name="status"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<ViewUserData> LoadUserList(string orgId,string userCode,string type,string roleId,string status,ParameterQuery param)
        {
            var dataUser = _dbSession.MD_UserRepository.Entities;
            var dataPerson = _dbSession.MD_PersonRepository.Entities;
            var dataSysUserRoleLink = _dbSession.Sys_UserRoleLinkRepository.Entities;
            var dataDictionary = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Category == "UserType");
            var dataCompany = _dbSession.MD_CompanyRepository.Entities;
            var dataWarehouse = _dbSession.MD_WarehouseRepository.Entities;

            if (!string.IsNullOrEmpty(userCode))
            {
                dataUser = dataUser.Where(u => u.UserCode.Contains(userCode));
            }
            if (!string.IsNullOrEmpty(status))
            {
                Boolean isValid = status == "1";
                dataUser = dataUser.Where(u => u.IsValid == isValid);
            }
            if (!string.IsNullOrEmpty(orgId))
            {
                int[] orgIdArr = new int[orgId.Split(',').Length];
                for (int i = 0; i < orgId.Split(',').Length; i++)
                {
                    orgIdArr[i] = int.Parse(orgId.Split(',')[i]);
                }
                dataPerson = from p in dataPerson
                             from oId in orgIdArr
                             where p.OrgId == oId
                             select p;
            }
            if (!string.IsNullOrEmpty(type))
            {
                dataPerson = dataPerson.Where(p => p.Type == type);
            }
            if (!string.IsNullOrEmpty(roleId) && roleId != "0")
            {
                dataSysUserRoleLink = dataSysUserRoleLink.Where(u => u.RoleID == int.Parse(roleId));
            }

            var data = from u in dataUser
                       join p in dataPerson on u.UserCode equals p.UserCode
                       join d in dataDictionary on p.Type equals d.Value
                       join c in dataCompany on p.CompanyCode equals c.CompanyCode
                       join w in dataWarehouse on p.WarehouseCode equals w.Code
                       //join s in dataSysUserRoleLink on u.UserCode equals s.UserCode into temp
                       //from tt in temp.DefaultIfEmpty()
                       select new ViewUserData
                       {
                           UserCode = u.UserCode,
                           FirstName = p.FirstName,
                           LastName = p.LastName,
                           Type = p.Type,
                           Company = p.CompanyCode,
                           CompanyName = c.CompanyName,
                           Warehouse = w.Code,
                           WarehouseName = w.Name,
                           TypeDesc = d.Description,
                           Email = p.Email,
                           Mobile = p.Mobile,
                           IsValid = u.IsValid,
                           IsLocked = u.IsLock,
                           NeedChangePwd = u.NeedChangePassword,
                           LastLoginTime = u.LastLoginTime,
                           LoginFailed = u.LoginFailed,
                           OrgID = p.OrgId
                       };
            param.total = data.Count();

            if (param.pageIndex > 0)
            {
                data = data.OrderBy(p => p.UserCode)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return data.AsQueryable().ToList();
        }

        public List<ViewUserData> LoadUserForRole(int roleId,string UserCode, ParameterQuery param)
        {
            var dataUser = _dbSession.MD_UserRepository.Entities;
            var dataPerson = _dbSession.MD_PersonRepository.Entities;
            var dataSysUserRoleLink = _dbSession.Sys_UserRoleLinkRepository.Entities.Where(p => p.RoleID == roleId && p.DeleteFlag == false);
            var dataDictionary = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Category == "UserType");
            var kris = dataDictionary.ToList();

            if (!string.IsNullOrEmpty(UserCode))
            {
                dataUser = dataUser.Where(p => p.UserCode.Contains(UserCode));
            }

            var data = from u in dataUser
                       join p in dataPerson on u.UserCode equals p.UserCode
                       join d in dataDictionary on p.Type equals d.Value
                       join s in dataSysUserRoleLink on u.UserCode equals s.UserCode into temp
                       from tt in temp.DefaultIfEmpty()
                       select new ViewUserData
                       {
                           UserCode = u.UserCode,
                           FirstName = p.FirstName,
                           LastName = p.LastName,
                           Type=p.Type,
                           TypeDesc = d.Description,
                           Email = p.Email,
                           Mobile = p.Mobile,
                           IsValid = u.IsValid,
                           IsLocked = u.IsLock,
                           NeedChangePwd = u.NeedChangePassword,
                           LastLoginTime = u.LastLoginTime,
                           LoginFailed = u.LoginFailed,
                           OrgID = p.OrgId,
                           HaveRole = tt.UserCode == null ? false : true
                       };

            param.total = data.Count();

            if (param.pageIndex > 0)
            {
                data = data.OrderByDescending(p => p.HaveRole)
                    .ThenBy(p => p.UserCode)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return data.AsQueryable().ToList();
        }

        /// <summary>
        /// load person by usercode
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public MD_Person LoadPerson(string userCode)
        {
            MD_Person person = _dbSession.MD_PersonRepository.Entities.Where(x => x.UserCode == userCode).FirstOrDefault();
            return person;
        }


        public MD_User CheckData(string UserCode)
        {
            MD_User data = _dbSession.MD_UserRepository.Entities.Where(x => x.UserCode == UserCode).SingleOrDefault();

            return data;
        }

        public MD_Person GetPersonByCode(string UserCode)
        {
            MD_Person data = _dbSession.MD_PersonRepository.Entities.Where(x => x.UserCode == UserCode).SingleOrDefault();
            return data;
        }

        public void AddUser(MD_User user, MD_Person person, List<Sys_UserRoleLink> userRoleListNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _dbSession.MD_UserRepository.AddEntities(user);
                _dbSession.MD_UserRepository.Commit();

                _dbSession.MD_PersonRepository.AddEntities(person);
                _dbSession.MD_PersonRepository.Commit();

                foreach (Sys_UserRoleLink item in userRoleListNew)
                {
                    _dbSession.Sys_UserRoleLinkRepository.AddEntities(item);
                    _dbSession.Sys_UserRoleLinkRepository.Commit();
                }

                scope.Complete();
            }
        }

        public void ModifiedUser(MD_User user, MD_Person person, List<Sys_UserRoleLink> userRoleListNew, List<Sys_UserRoleLink> userRoleListOld)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _dbSession.MD_UserRepository.UpdateEntities(user);
                _dbSession.MD_UserRepository.Commit();

                _dbSession.MD_PersonRepository.UpdateEntities(person);
                _dbSession.MD_PersonRepository.Commit();

                foreach (Sys_UserRoleLink item in userRoleListOld)
                {
                    item.DeleteFlag = true;
                    item.Modifier = user.LastUpdateUser;
                    item.ModifiedTime = DateTime.Now;

                    _dbSession.Sys_UserRoleLinkRepository.UpdateEntities(item);
                    _dbSession.Sys_UserRoleLinkRepository.Commit();
                }

                foreach (Sys_UserRoleLink item in userRoleListNew)
                {
                    Sys_UserRoleLink data = _dbSession.Sys_UserRoleLinkRepository.Entities.Where(p => p.UserCode == item.UserCode && p.RoleID == item.RoleID).FirstOrDefault();
                    if (data == null)
                    {
                        _dbSession.Sys_UserRoleLinkRepository.AddEntities(item);
                    }
                    else
                    {
                        data.DeleteFlag = false;
                        data.Modifier = user.LastUpdateUser;
                        data.ModifiedTime = DateTime.Now;

                        _dbSession.Sys_UserRoleLinkRepository.UpdateEntities(data);
                    }
                    _dbSession.Sys_UserRoleLinkRepository.Commit();
                }

                scope.Complete();
            }
        }

        public OperationResult DeleteUser(MD_User data)
        {
            OperationResult result = new OperationResult();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _dbSession.MD_UserRepository.UpdateEntities(data);
                    _dbSession.MD_UserRepository.Commit();

                    scope.Complete();
                }
            }
            catch (Exception error)
            {
                result.ResultType = OperationResultType.failed;
                result.Message = error.Message;
            }
            return result;
        }

        public List<ViewComboboxStringData> GetPersonList(string UserType, bool NeedSum)
        {
            var dataUser = _dbSession.MD_UserRepository.Entities.Where(p => p.IsValid == true);
            var dataPerson = _dbSession.MD_PersonRepository.Entities;
            var dataDic = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Valid == true && p.Category == "UserType");
            if (!string.IsNullOrEmpty(UserType))
            {
                dataDic = dataDic.Where(p => p.Value == UserType);
            }
            var data = (from p in dataPerson
                        join u in dataUser on p.UserCode equals u.UserCode
                        join d in dataDic on p.Type equals d.Value
                        select new ViewComboboxStringData
                        {
                            name = p.UserCode,
                            value = p.UserCode
                        }).ToList();
            if (NeedSum)
            {
                DateTime nowDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                foreach (ViewComboboxStringData item in data)
                {
                    var assign = _dbSession.DSD_T_ShipmentAssignRepository.Entities.Where(p => p.Driver == item.value && p.WorkDay == nowDate);
                    item.name = item.name + "(" + assign.Count() + ")";
                }
            }

            return data;
        }


        public List<ViewComboboxStringData> GetDriverList()
        {
            var dataUser = _dbSession.MD_UserRepository.Entities.Where(p => p.IsValid == true);
            var dataPerson = _dbSession.MD_PersonRepository.Entities;
            var dataDic = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Valid == true && p.Category == "UserType");
            dataDic = dataDic.Where(p => p.Value == "1");
            var dataTD = _dbSession.DSD_M_TruckDriverRepository.Entities;
            
            var data = (from p in dataPerson
                        join u in dataUser on p.UserCode equals u.UserCode
                        join d in dataDic on p.Type equals d.Value
                        select new ViewComboboxStringData
                        {
                            name = p.UserCode,
                            value = p.UserCode
                        }).ToList();
            foreach (ViewComboboxStringData item in data)
            {
                var assign = _dbSession.DSD_M_TruckDriverRepository.Entities.Where(p => p.Driver == item.value).FirstOrDefault();
                if (assign != null)
                {
                    var truck = _dbSession.DSD_M_TruckRepository.Entities.Where(p => p.ID == assign.TruckId).FirstOrDefault();
                    item.name = item.name + "(" + truck.TruckCode + ")";
                }
            }
            return data;
        }
    }
}
