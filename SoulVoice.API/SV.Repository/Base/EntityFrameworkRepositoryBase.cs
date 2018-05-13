using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using SV.Repository.Core;
using Z.EntityFramework.Plus;

namespace SV.Repository.Base
{
    public class EntityFrameworkRepositoryBase<TEntity> : IEntityFrameworkCommandRepository<TEntity>//, IDisposable
         where TEntity : class
    {
        private readonly ThreadLocal<EntityFrameworkContext> _localCtx = new ThreadLocal<EntityFrameworkContext>(() => new EntityFrameworkContext());

        public EntityFrameworkContext DbContext => _localCtx.Value;


        public void Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbContext.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public int Update(Expression<Func<TEntity, TEntity>> updateExpression)
        {
            return DbContext.Set<TEntity>().Update(updateExpression);
        }

        public int Update(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            return DbContext.Set<TEntity>().Where(filterExpression).Update(updateExpression);
        }
      

        public void Delete(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Deleted;
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Delete(ICollection<TEntity> entityCollection)
        {
            if (entityCollection.Count == 0)
                return;
            DbContext.Set<TEntity>().Attach(entityCollection.First());
            DbContext.Set<TEntity>().RemoveRange(entityCollection);
        }

        public int Delete(Expression<Func<TEntity, bool>> filterExpression)
        {
            return DbContext.Set<TEntity>().Where(filterExpression).Delete();
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        //public void Dispose()
        //{
        //    _context.Dispose();
        //    this.Dispose();
        //}


    }
}
