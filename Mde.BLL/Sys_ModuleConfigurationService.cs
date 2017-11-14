using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mde.IBLL;
using Mde.Model.DataModel;
using System.Transactions;

namespace Mde.BLL
{
    public partial class Sys_ModuleConfigurationService : BaseService<Sys_ModuleConfiguration>, ISys_ModuleConfigurationService
    {
        public List<Sys_ModuleConfiguration> LoadSysModuleConfigurationInfo(int RoleID)
        {
            var data = _dbSession.Sys_ModuleConfigurationRepository.Entities.Where(p => p.RoleID == RoleID && p.Activated == true);

            return data.ToList();
        }

        /// <summary>
        /// 重复性校验数据
        /// </summary>
        /// <param name="EventId">EventId</param>
        /// <returns></returns>
        public Sys_ModuleConfiguration CheckData(int RoleID, int ModuleId)
        {
            Sys_ModuleConfiguration data = _dbSession.Sys_ModuleConfigurationRepository.Entities.Where(p => p.RoleID == RoleID && p.ModuleID == ModuleId).SingleOrDefault();

            return data;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="data">数据</param>
        public void AddData(Sys_ModuleConfiguration data)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _dbSession.Sys_ModuleConfigurationRepository.AddEntities(data);
                _dbSession.Sys_ModuleConfigurationRepository.Commit();

                scope.Complete();
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data">数据</param>
        public void UpdateData(Sys_ModuleConfiguration data)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _dbSession.Sys_ModuleConfigurationRepository.UpdateEntities(data);
                _dbSession.Sys_ModuleConfigurationRepository.Commit();

                scope.Complete();
            }
        }
    }
}
