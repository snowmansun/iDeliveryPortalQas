using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mde.Interface;

namespace Mde.Implement
{
    /// <summary>
    /// 数据库交互会话，
    /// 如果操作数据库的话直接从这里来操作
    /// </summary>
    public partial class DbSession : IDbSession //代表的是应用程序跟数据库之间的一次会话，也是数据库访问层的统一入口
    {

        //public IStoreRepository StoreRepository { get { return new StoreRepository(); } }


        /// <summary>
        /// 代表当前应用程序跟数据库的回话内所有的实体变化，更新会数据库
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()  //UintWork单元工作模式
        {
            //调用EF上下文的SaveChanges的方法
            return EFContextFactory.GetCurrentDbContext().SaveChanges();

        }

        //如何执行SQL语句呢？？？？
        //public int ExcuteSql(string strSql, DbParameter[] parameters)
        //{
        //封装一个执行的SQL脚本
        //    return EFContextFactory.GetCurrentDbContext().ExecuteFunction(strSql, parameters);
        //}

        public int ExcuteSql(string strSql, DbParameter[] parameters)
        {
            DbContext dbcontext = EFContextFactory.GetCurrentDbContext();
            int result;

            if (parameters == null)
                result = dbcontext.Database.SqlQuery<int>(strSql).FirstOrDefault();
            else
                result = dbcontext.Database.SqlQuery<int>(strSql, parameters).FirstOrDefault();
            return result;
        }

        public int ExcuteNoQuery(string strSql, DbParameter[] parameters)
        {
            DbContext dbcontext = EFContextFactory.GetCurrentDbContext();
            int result;

            if (parameters == null)
                result = dbcontext.Database.ExecuteSqlCommand(strSql);
            else
                result = dbcontext.Database.ExecuteSqlCommand(strSql, parameters);
            return result;
        }


        /// <summary>  
        /// 执行原始SQL命令  
        /// </summary>  
        /// <param name="commandText">SQL命令</param>  
        /// <param name="parameters">参数</param>  
        /// <returns>影响的记录数</returns>  
        public IList<T> ExecuteSqlNonQuery<T>(string commandText, params Object[] parameters)
        {
            DbContext dbcontext = EFContextFactory.GetCurrentDbContext();
            var results = dbcontext.Database.SqlQuery<T>(commandText, parameters);
            return results.ToList<T>();
        }
    }
}
