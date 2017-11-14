using Mde.Model.DataModel;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.IBLL
{
    public partial interface ISys_ModuleService : IBaseService<Sys_Module>
    {
        List<Sys_Module> LoadSysModuleInfo();

        List<ViewModuleData> GetMenuList(int roleID);
    }
}
