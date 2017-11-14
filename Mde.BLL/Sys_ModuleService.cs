using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mde.IBLL;
using Mde.Model.DataModel;
using System.Data.Common;
using Mde.ModelView.ModelView;

namespace Mde.BLL
{
    public partial class Sys_ModuleService : BaseService<Sys_Module>, ISys_ModuleService
    {
        public List<Sys_Module> LoadSysModuleInfo()
        {
            IQueryable<Sys_Module> rs = _dbSession.Sys_ModuleRepository.Entities;
            return rs.AsQueryable().ToList(); 
        }

        public List<ViewModuleData> GetMenuList(int RoleId)
        {
            string sql = string.Format(@"SELECT A.ModuleID,A.ModuleCode,A.ModuleName,A.Description,B.Action ButtonRole,
                                         A.ParentModule,A.ModulePath,A.Sequence,A.Url,A.ClientScope,A.Action,A.ActionMode
                                         FROM [dbo].[Sys_Module] A
		                                    LEFT JOIN [Sys_ModuleConfiguration] b ON a.ModuleID=b.ModuleID AND b.Activated=1 AND b.RoleID={0}
                                         WHERE A.ParentModule Is Null OR 
                                         EXISTS (SELECT 1 FROM [dbo].[Sys_ModuleConfiguration] B 
                                         WHERE A.ModuleID = B.ModuleID AND B.Activated = 1 AND B.RoleID = {0})", RoleId);
            DbParameter[] parameters = new DbParameter[1];
            IList<ViewModuleData> LstInfo = _dbSession.ExecuteSqlNonQuery<ViewModuleData>(sql, parameters);
            var data = from x in LstInfo
                       select new ViewModuleData
                       {
                           ModuleID = x.ModuleID,
                           ModuleCode = x.ModuleCode,
                           ModuleName = x.ModuleName,
                           Description = x.Description,
                           ParentModule = x.ParentModule,
                           ModulePath = x.ModulePath,
                           Sequence = x.Sequence,
                           Url = x.Url,
                           Icon = x.Icon,
                           ClientScope = x.ClientScope,
                           Action = x.Action,
                           ActionMode = x.ActionMode,
                           ButtonRole = x.ButtonRole
                       };
            return data.ToList();
        }
    }
}
