using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using DapperExtensions;
using SV.Common.Extensions;
using SV.Entity.Query;
using SV.Repository.Base;
using SV.Repository.Core.Query;

namespace SV.Repository.Query
{
    public class UserQueryRepository : DapperRepositoryBase<User>, IUserQueryRepository
    {

        public List<User> GetAll()
        {
            var sql = @"SELECT * FROM User AS u 
                        LEFT JOIN UserRole AS ur ON u.Id=ur.UserId
                        LEFT JOIN Role AS r ON ur.RoleId=r.Id";
           
            return GetListBySql(sql);
        }

        public User GetById(long userId)
        {
            var sql = $@"SELECT * FROM User AS u 
                        LEFT JOIN UserRole AS ur ON u.Id=ur.UserId
                        LEFT JOIN Role AS r ON ur.RoleId=r.Id
                        WHERE u.Id={userId}";
            return GetSingleBySql(sql);
        }

        public List<User> GetPage(int pageNum, int pageSize, out long outTotal, Expression<Func<User, bool>> expression = null, object sortList = null)
        {
            var baseSql = @"SELECT * FROM User AS u 
                    LEFT JOIN UserRole AS ur ON u.Id = ur.UserId
                    LEFT JOIN Role AS r ON ur.RoleId = r.Id";

            var commandSql = GetPageSql(baseSql,"User","u", pageNum, pageSize);

            var lookup = new Dictionary<long, User>();
            using (var multi = Conn.QueryMultiple(commandSql))
            {
                outTotal = multi.Read<int>().Single();
                multi.Read(FillDic(lookup), splitOn: "Id");
                return lookup.Values.ToList();
            }

            // IPredicateGroup predicate = DapperLinqBuilder<User>.FromExpression(expression);
            // IList<ISort> sort = SortConvert(sortList);
            //// Conn.Query<>()

            // ExpressionToSql sql = new ExpressionToSql();

            // Expression<FillDic<User, bool>> aaa = u => u.Id == 1 && u.Name.DB_NotLike("123");

            // Expression<FillDic<User, bool>> bbb = u => u.Id == 1 && u.Name.DB_Like("aa");

            // var ss = LambdaToSqlHelper.GetWhereSql(aaa); ;
            // var dsds = sql.GetSql(bbb); ;
            // var ddd = sql.GetSql(expression);
            //throw new NotImplementedException();
        }

        
        
        private List<User> GetListBySql(string sql)
        {
            var lookup = new Dictionary<long, User>();
            Conn.Query(sql, FillDic(lookup),splitOn: "Id");
            return lookup.Values.ToList();
        }

        private User GetSingleBySql(string sql)
        {
            var lookup = new Dictionary<long, User>();
            Conn.Query(sql, FillDic(lookup),splitOn: "Id");
            return lookup.Values.FirstOrDefault();
        }

        private Func<User, UserRole, Role, User> FillDic(Dictionary<long, User> lookup)
        {
            return (u, ur, r) =>
            {
                if (!lookup.TryGetValue(u.Id, out var tmp))
                {
                    tmp = u;
                    lookup.Add(u.Id, tmp);
                }

                var tmpUr = tmp.UserRoles.FirstOrDefault(p => p.Id == ur.Id);
                if (tmpUr == null)
                {
                    tmpUr = ur;
                    tmp.UserRoles.Add(tmpUr);
                    if (tmpUr != null)
                    {
                        var tmpR = tmpUr.Role;
                        if (tmpR == null)
                        {
                            tmpUr.Role = r;
                        }
                    }

                }
                return u;
            };
        }
    }
}