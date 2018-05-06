﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SV.Repository.Core;
using Z.EntityFramework.Plus;

namespace SV.Repository.Base
{
    public class EntityFrameworkRepositoryBase<TEntity> : IEntityFrameworkCommandRepository<TEntity>, IDisposable
         where TEntity : class
    {
        private readonly EntityFrameworkContext _context;

        public EntityFrameworkRepositoryBase(EntityFrameworkContext context)
        {
            _context = context;
        }
        


        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }


        public void Update(Expression<Func<TEntity, TEntity>> updateExpression)
        {
            _context.Set<TEntity>().Update(updateExpression);
        }

        public void Update(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            _context.Set<TEntity>().Where(filterExpression).Update(updateExpression);
        }
      

        public void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete(ICollection<TEntity> entityCollection)
        {
            if (entityCollection.Count == 0)
                return;
            _context.Set<TEntity>().Attach(entityCollection.First());
            _context.Set<TEntity>().RemoveRange(entityCollection);
        }

        public int Delete(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _context.Set<TEntity>().Where(filterExpression).Delete();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            this.Dispose();
        }


    }
}
