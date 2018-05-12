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
using System.Threading.Tasks;

namespace SV.Repository.Base
{
    public class DapperRepositoryBase<TEntity> : IDapperQueryRepository<TEntity>//,IDisposable
        where TEntity : class, IEntity
    {

        protected readonly DapperContext Context;

        public DapperRepositoryBase(DapperContext context)
        {
            Context = context;
        }

        #region 数据查询
        public TEntity FindSingle(object id)
        {
            using (var db = Context.GetConnection())
            {
                return db.Get<TEntity>(id);
            }

        }

        public TEntity FindSingle(Expression<Func<TEntity, bool>> expression = null, object sortList = null)
        {
            using (var db = Context.GetConnection())
            {
                var predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                var sort = SortConvert(sortList);
                var data = db.GetSet<TEntity>(predicate, sort, 0, 1);
                return data.FirstOrDefault();
            }
        }

        public IEnumerable<TEntity> FindAll(object sortList = null)
        {
            return Find(null,sortList);
        }


        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression, object sortList = null)
        {
            using (var db = Context.GetConnection())
            {
                IList<ISort> sort = SortConvert(sortList);
                if (expression == null)
                {
                    return db.GetList<TEntity>(null, sort, transaction: db.BeginTransaction(IsolationLevel.ReadUncommitted));
                }
                else
                {
                    var predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                    return db.GetList<TEntity>(predicate, sort, transaction: db.BeginTransaction(IsolationLevel.ReadUncommitted));
                }
            }

        }

        public IEnumerable<TEntity> Page(int pageNum, int pageSize, out long outTotal,
            Expression<Func<TEntity, bool>> expression = null, object sortList = null)
        {
            using (var db = Context.GetConnection())
            {
                IPredicateGroup predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                IList<ISort> sort = SortConvert(sortList);
                var entities = db.GetPage<TEntity>(predicate, sort, pageNum-1, pageSize, transaction: db.BeginTransaction(IsolationLevel.ReadUncommitted));
                outTotal = db.Count<TEntity>(predicate);
                return entities;
            }

        }

        public long Count(Expression<Func<TEntity, bool>> expression = null)
        {
            using (var db = Context.GetConnection())
            {
                var predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                return db.Count<TEntity>(predicate);
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
            using (var db = Context.GetConnection())
            {
                return db.GetAsync<TEntity>(id);
            }

        }
        
        public Task<IEnumerable<TEntity>> FindAllAsync(object sortList = null)
        {
            return FindAsync(null, sortList);
        }
        
        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, object sortList = null)
        {
            using (var db = Context.GetConnection())
            {
                IList<ISort> sort = SortConvert(sortList);
                if (expression == null)
                {
                    return db.GetListAsync<TEntity>(null, sort, transaction: db.BeginTransaction(IsolationLevel.ReadUncommitted));
                }
                else
                {
                    var predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                    return db.GetListAsync<TEntity>(predicate, sort, transaction: db.BeginTransaction(IsolationLevel.ReadUncommitted));
                }
            }

        }

        public Task<IEnumerable<TEntity>> PageAsync(int pageNum, int pageSize, out Task<int> outTotal,
            Expression<Func<TEntity, bool>> expression = null, object sortList = null)
        {
            using (var db = Context.GetConnection())
            {
                IPredicateGroup predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                IList<ISort> sort = SortConvert(sortList);
                var entities = db.GetPageAsync<TEntity>(predicate, sort, pageNum - 1, pageSize, transaction: db.BeginTransaction(IsolationLevel.ReadUncommitted));
                outTotal = db.CountAsync<TEntity>(predicate);
                return entities;
            }

        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            using (var db = Context.GetConnection())
            {
                var predicate = DapperLinqBuilder<TEntity>.FromExpression(expression);
                return db.CountAsync<TEntity>(predicate);
            }
        }
        
        #endregion
        

        #region 辅助方法
        /// <summary>
        /// 转换成Dapper排序方式
        /// </summary>
        /// <param name = "sortList" ></param >
        /// < returns ></returns >
        private static IList<ISort> SortConvert(object sortList)
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

        //public void Dispose()
        //{
        //    Context.Dispose();
        //    this.Dispose();
        //}
        #endregion

    }
}
