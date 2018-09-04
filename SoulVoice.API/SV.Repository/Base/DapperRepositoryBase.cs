using SV.Entity.Auditing;
using SV.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using DapperExtensions;
using System.Data;
using System.Threading;
using System.Threading.Tasks;


namespace SV.Repository.Base
{
    public class DapperRepositoryBase<TEntity> : IDapperQueryRepository<TEntity>,IDisposable
        where TEntity : class, IEntity
    {

        private readonly ThreadLocal<DapperContext> _localCtx = new ThreadLocal<DapperContext>(() => new DapperContext());

        public IDbConnection Conn => _localCtx.Value.Conn;

        #region 数据查询
        public TEntity FindSingle(object id)
        {
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                return Conn.Get<TEntity>(id);
            }
            finally
            {
                Conn.Close();
            }
        }

        public TEntity FindSingle(Expression<Func<TEntity, bool>> expression = null, object sortList = null)
        {
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                var predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                var sort = SortConvert(sortList);
                var data = Conn.GetSet<TEntity>(predicate, sort, 0, 1);
                return data.FirstOrDefault();
            }
            finally
            {
                Conn.Close();
            }
        }

        public IEnumerable<TEntity> FindAll(object sortList = null)
        {
            return Find(null,sortList);
        }


        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression, object sortList = null)
        {
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                IList<ISort> sort = SortConvert(sortList);
                if (expression == null)
                {
                    return Conn.GetList<TEntity>(null, sort);
                }
                else
                {
                    var predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                    return Conn.GetList<TEntity>(predicate, sort);
                }
            }
            finally
            {
                Conn.Close();
            }
        }

        public IEnumerable<TEntity> Page(int pageNum, int pageSize, out long outTotal,
            Expression<Func<TEntity, bool>> expression = null, object sortList = null)
        {
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                IPredicateGroup predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                IList<ISort> sort = SortConvert(sortList);
                var entities = Conn.GetPage<TEntity>(predicate, sort, pageNum - 1, pageSize);
                outTotal = Conn.Count<TEntity>(predicate);
                return entities;
            }
            finally
            {
                Conn.Close();
            }
            
        }

        public long Count(Expression<Func<TEntity, bool>> expression = null)
        {
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                var predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                return Conn.Count<TEntity>(predicate);
            }
            finally
            {
                Conn.Close();
            }
         
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            var ct = Count(expression);
            return ct > 0;
        }

        #endregion

        #region 数据查询异步
        public Task<TEntity> FindSingleAsync(object id)
        {
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                return Conn.GetAsync<TEntity>(id);
            }
            finally
            {
                Conn.Close();
            }
        }

        public Task<IEnumerable<TEntity>> FindAllAsync(object sortList = null)
        {
            return FindAsync(null, sortList);
        }

        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, object sortList = null)
        {
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                IList<ISort> sort = SortConvert(sortList);
                if (expression == null)
                {
                    return Conn.GetListAsync<TEntity>(null, sort);
                }
                else
                {
                    var predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                    return Conn.GetListAsync<TEntity>(predicate, sort);
                }
            }
            finally
            {
                Conn.Close();
            }
           
        }

        public Task<IEnumerable<TEntity>> PageAsync(int pageNum, int pageSize, out Task<int> outTotal,
            Expression<Func<TEntity, bool>> expression = null, object sortList = null)
        {
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                IPredicateGroup predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                IList<ISort> sort = SortConvert(sortList);
                var entities = Conn.GetPageAsync<TEntity>(predicate, sort, pageNum - 1, pageSize);
                outTotal = Conn.CountAsync<TEntity>(predicate);
                return entities;
            }
            finally
            {
                Conn.Close();
            }
            
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
                var predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                return Conn.CountAsync<TEntity>(predicate);
            }
            finally
            {
                Conn.Close();
            }
        }

        #endregion


        #region 辅助方法
        /// <summary>
        /// 转换成Dapper排序方式
        /// </summary>
        /// <param name = "sortList" ></param >
        /// < returns ></returns >
        protected static IList<ISort> SortConvert(object sortList)
        {
            IList<ISort> sorts = new List<ISort>();
            if (sortList == null)
            {
                sorts.Add(Predicates.Sort<TEntity>(f => f.Id, false));//默认以Id asc=flase 降序
                return sorts;
            }

            Type obj = sortList.GetType();
            var fields = obj.GetRuntimeFields();
            foreach (FieldInfo f in fields)
            {
                var s = new Sort();
                var mt = Regex.Match(f.Name, @"^\<(.*)\>.*$");
                s.PropertyName = mt.Groups[1].Value;
                s.Ascending = f.GetValue(sortList) == null || (bool)f.GetValue(sortList);
                sorts.Add(s);
            }

            return sorts;
        }

        public void Dispose()
        {
            Conn.Dispose();
            _localCtx.Dispose();
        }
        #endregion

        protected string GetPageSql(string sql,string mainTableName, string mainTableAlias, int pageNum, int pageSize)
        {
            var cSql = $"SELECT COUNT(*) FROM {mainTableName}";
            var dSql =
                $"{sql} JOIN (SELECT Id FROM {mainTableName} LIMIT {pageNum-1}, {pageSize}) p ON {mainTableAlias}.Id = p.Id ";
            return cSql + ";" + dSql;
        }

	    protected string GetIdsString(List<long> ids)
	    {
		    var result = ids.Aggregate("", (current, id) => current + (id + ","));
		    return result.Substring(0, result.Length - 1);
	    }

	}
    
}
