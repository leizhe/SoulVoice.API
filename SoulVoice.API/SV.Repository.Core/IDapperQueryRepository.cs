﻿using System;
using System.Linq;
using System.Linq.Expressions;
using SV.Common.Extensions;

namespace SV.Repository.Core

{
    public interface IDapperQueryRepository<TEntity> where TEntity : class
    {

        TEntity FindSingle(Expression<Func<TEntity, bool>> exp = null);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> exp = null);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);

        IQueryable<TEntity> FindQueryable(IQueryable<TEntity> q, Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);

        int GetCount(Expression<Func<TEntity, bool>> exp = null);


    }
}