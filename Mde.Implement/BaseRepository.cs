using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.Implement
{
    public partial class BaseRepository<T> where T : class
    {
        //获取的实当前线程内部的上下文实例，而且保证了线程内上下文实例唯一
        private DbContext db = EFContextFactory.GetCurrentDbContext();

        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        public virtual IQueryable<T> Entities
        {
            get { return db.Set<T>(); }
        }

        //添加
        public T AddEntities(T entity)
        {
            db.Entry<T>(entity).State = EntityState.Added;
            //db.SaveChanges();
            return entity;
        }

        //修改
        public bool UpdateEntities(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Modified;
            //db.Entry<T>(entity).State= EntityState.Unchanged;
            //return db.SaveChanges() > 0;
            return true;
        }

        //修改
        public bool DeleteEntities(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Deleted;
            //return db.SaveChanges() > 0;
            return true;
        }

        //FIND
        public T FindEntities(string id)
        {
            return db.Set<T>().Find(id);
        }

        //查询
        public IQueryable<T> LoadEntities(Func<T, bool> wherelambda)
        {
            return db.Set<T>().Where<T>(wherelambda).AsQueryable();
        }

        //分页
        public IQueryable<T> LoadPagerEntities<S>(int pageSize, int pageIndex, out int total,
            Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {
            var tempData = db.Set<T>().Where<T>(whereLambda);

            total = tempData.Count();

            //排序获取当前页的数据
            if (isAsc)
            {
                tempData = tempData.OrderBy<T, S>(orderByLambda).
                      Skip<T>(pageSize * (pageIndex - 1)).
                      Take<T>(pageSize).AsQueryable();
            }
            else
            {
                tempData = tempData.OrderByDescending<T, S>(orderByLambda).
                     Skip<T>(pageSize * (pageIndex - 1)).
                     Take<T>(pageSize).AsQueryable();
            }
            return tempData.AsQueryable();
        }
        // 提交
        public bool Commit()
        {
            return db.SaveChanges() > 0;
        }
    }
}
