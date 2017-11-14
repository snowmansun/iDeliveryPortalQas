using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mde.IBLL;
using Mde.Model.DataModel;

namespace Mde.BLL
{
    public partial class Sys_PersonService : BaseService<Sys_Person>, ISys_PersonService
    {
        public IQueryable<Sys_Person> LoadSysPersonInfo(string userCode)
        {
            IQueryable<Sys_Person> rs;

            rs = _dbSession.Sys_PersonRepository.LoadEntities(x => x.UserName == userCode);

            return rs.AsQueryable();
        }
    }
}
