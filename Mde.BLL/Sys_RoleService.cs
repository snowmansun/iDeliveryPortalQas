using Mde.Common;
using Mde.IBLL;
using Mde.Model.DataModel;
using Mde.Model.ModelView;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Mde.BLL
{
    public partial class Sys_RoleService : BaseService<Sys_Role>, ISys_RoleService
    {

        public List<Sys_Role> LoadSysRoleInfo(string RoleNm)
        {
            IQueryable<Sys_Role> rs = _dbSession.Sys_RoleRepository.Entities;

            if (!string.IsNullOrEmpty(RoleNm))
                rs = rs.Where(p => p.Name == RoleNm);

            return rs.AsQueryable().ToList();
        }

        /// <summary>
        /// 获取下拉框数据
        /// </summary>
        /// <returns></returns>
        public List<ViewComboboxIntData> GetComboboxData()
        {
            var dataSysRole = _dbSession.Sys_RoleRepository.Entities.Where(p => p.DeleteFlag == false);
            var data = from x in dataSysRole
                       select new ViewComboboxIntData
                       {
                           value = x.RoleID,
                           name = x.Name
                       };

            return data.ToList();
        }

        public List<ViewUserRoleData> GetRoleData(ParameterQuery param, string UserCode)
        {
            var dataRole = _dbSession.Sys_RoleRepository.Entities.Where(p => p.DeleteFlag == false);
            var dataLink = _dbSession.Sys_UserRoleLinkRepository.Entities.Where(p => p.DeleteFlag == false);
            dataLink = dataLink.Where(p => p.UserCode == UserCode);

            var data = from r in dataRole
                       join l in dataLink on r.RoleID equals l.RoleID into tmp
                       from tt in tmp.DefaultIfEmpty()
                       select new ViewUserRoleData
                       {
                           RoleID = r.RoleID,
                           Name = r.Name,
                           Description = r.Description,
                           IsHavingRole = tt.UserCode == null ? false : true
                       };


            param.total = data.Count();

            if (param.pageIndex > 0)
            {
                data = data.OrderBy(p => p.Name)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return data.AsQueryable().ToList();
        }


        /// <summary>
        /// 重复性校验数据
        /// </summary>
        /// <param name="RoleId">RoleId</param>
        /// <returns></returns>
        public Sys_Role CheckData(int RoleId)
        {
            Sys_Role data = _dbSession.Sys_RoleRepository.Entities.Where(p => p.RoleID == RoleId).SingleOrDefault();

            return data;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data">数据</param>
        public void AddData(Sys_Role data)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _dbSession.Sys_RoleRepository.AddEntities(data);
                _dbSession.Sys_RoleRepository.Commit();

                scope.Complete();
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data">数据</param>
        public void UpdateData(Sys_Role data)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _dbSession.Sys_RoleRepository.UpdateEntities(data);
                _dbSession.Sys_RoleRepository.Commit();

                scope.Complete();
            }
        }
    }
}
