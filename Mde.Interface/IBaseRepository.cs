using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.Interface
{
    /// <summary>
    /// 基仓储实现的方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T:class,new()
    {
        //获取 当前实体的查询数据集
        IQueryable<T> Entities { get; }

        //添加
        T AddEntities(T entity);

        //修改
        bool UpdateEntities(T entity);


        //修改
        bool DeleteEntities(T entity);


        T FindEntities(string id);

        //查询
        IQueryable<T> LoadEntities(Func<T, bool> wherelambda);


        //分页
        IQueryable<T> LoadPagerEntities<S>(int pageSize, int pageIndex,
            out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda);

        bool Commit();
    }
}
