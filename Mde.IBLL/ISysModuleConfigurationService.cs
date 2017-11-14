using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mde.Model.DataModel;
using Mde.Common;

namespace Mde.IBLL
{
    public partial interface ISys_ModuleConfigurationService : IBaseService<Sys_ModuleConfiguration>
    {
        /// <summary>
        /// 数据取得
        /// </summary>
        List<Sys_ModuleConfiguration> LoadSysModuleConfigurationInfo(int RoleID);

        Sys_ModuleConfiguration CheckData(int RoleID, int ModuleId);

        void AddData(Sys_ModuleConfiguration data);

        void UpdateData(Sys_ModuleConfiguration data);
    }
}
