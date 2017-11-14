using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mde.Model.DataModel;

namespace Mde.IBLL
{
    public partial interface ISys_PersonService : IBaseService<Sys_Person>
    {
        IQueryable<Sys_Person> LoadSysPersonInfo(string userCode);
    }
}
