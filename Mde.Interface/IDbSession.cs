using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.Interface
{
    //添加接口，起约束作用
    public partial interface IDbSession
    {

        //IStoreRepository StoreRepository { get; }

        int SaveChanges();

        int ExcuteSql(string strSql, DbParameter[] parameters);

        int ExcuteNoQuery(string strSql, DbParameter[] parameters);

        IList<T> ExecuteSqlNonQuery<T>(string commandText, params Object[] parameters);
    }
}
