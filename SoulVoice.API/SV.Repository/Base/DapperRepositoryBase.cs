using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using DapperExtensions;
using SV.Entity.Auditing;
using SV.Repository.Core;

namespace SV.Repository.Base
{
    public class DapperRepositoryBase<TEntity> : IDapperQueryRepository<TEntity>,IDisposable
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
      
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression, object sortList = null)
        {
            using (var db = Context.GetConnection())
            {
                IList<ISort> sort = SortConvert(sortList);//转换排序接口
                if (expression == null)
                {
                    //允许脏读
                    return db.GetList<TEntity>(null, sort, transaction: db.BeginTransaction(IsolationLevel.ReadUncommitted));//如果条件为Null 就查询所有数据
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
                IPredicateGroup predicate = DapperLinqBuilder<TEntity>.FromExpression(expression); //转换Linq表达式
                IList<ISort> sort = SortConvert(sortList);//转换排序接口
                var entities = db.GetPage<TEntity>(predicate, sort, pageNum, pageSize, transaction: db.BeginTransaction(IsolationLevel.ReadUncommitted));
                outTotal = db.Count<TEntity>(null);
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

        #region 辅助方法
        /// <summary>
        /// 转换成Dapper排序方式
        /// </summary>
        /// <param name="sortList"></param>
        /// <returns></returns>
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

        public void Dispose()
        {
            Context.Dispose();
            this.Dispose();
        }
        #endregion

    }
}
