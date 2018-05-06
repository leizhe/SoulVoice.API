using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SV.Common.Extensions;

namespace SV.Repository.Core

{
    public interface IDapperQueryRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 根据Id获取实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindSingle(object id);
        /// <summary>
        /// 获取结果集第一条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sortList"></param>
        /// <returns></returns>
        TEntity FindSingle(Expression<Func<TEntity, bool>> expression = null, object sortList = null);
        /// <summary>
        /// 根据条件获取表数据
        /// </summary>
        /// <param name="expression">linq表达式</param>
        /// <param name="sortList">排序</param>
        /// <returns></returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression, object sortList = null);
        /// <summary>
        /// 数据表 分页
        /// </summary>
        /// <param name="pageNum">指定页数 索引从0开始</param>
        /// <param name="pageSize">指定每页多少项</param>
        ///<param name="outTotal">输出当前表的总项数</param>
        /// <param name="expression">条件 linq表达式 谓词</param>
        /// <param name="sortList">排序字段</param>
        /// <returns></returns>
        IEnumerable<TEntity> Page(int pageNum, int pageSize, out long outTotal,
            Expression<Func<TEntity, bool>> expression = null, object sortList = null);
        /// <summary>
        /// 获取数据表总项数
        /// </summary>
        /// <param name="expression">linq表达式 谓词</param>
        /// <returns></returns>
        long Count(Expression<Func<TEntity, bool>> expression = null);
        /// <summary>
        /// 查看指定的数据是否存在
        /// </summary>
        /// <param name="expression">linq表达式 谓词</param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> expression);


    }
}