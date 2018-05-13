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
            var lookup = new Dictionary<long, User>();
            Conn.Query<User, UserRole, Role, User>(sql, (u, ur, r) =>
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
                        tmpUr.Role = r;
                    }

                    return u;
                },
                splitOn: "Id");
            return lookup.Values.ToList();
        }

        public User GetById(long userId)
        {
            var sql = $@"SELECT * FROM User AS u 
                        LEFT JOIN UserRole AS ur ON u.Id=ur.UserId
                        LEFT JOIN Role AS r ON ur.RoleId=r.Id
                        WHERE u.Id={userId}";
            var lookup = new Dictionary<long, User>();
            Conn.Query<User, UserRole, Role, User>(sql, (u, ur, r) =>
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
                        if (tmpUr!=null)
                        {
                            var tmpR = tmpUr.Role;
                            if (tmpR == null)
                            {
                                tmpUr.Role = r;
                            }
                        }
                      
                    }
                    

                    return u;
                },
                splitOn: "Id");
            return lookup.Values.FirstOrDefault();
        }

        public List<User> GetPage(int pageNum, int pageSize, out long outTotal, Expression<Func<User, bool>> expression = null, object sortList = null)
        {
            var baseSql = @"SELECT * FROM User AS u 
                    LEFT JOIN UserRole AS ur ON u.Id = ur.UserId
                    LEFT JOIN Role AS r ON ur.RoleId = r.Id";
            var sql =GetPageSql(baseSql, 0, 10);


            using (var multi = Conn.QueryMultiple(sql, new { UserId = 1 }))
            {
                var user = multi.Read<User>().ToList();
                outTotal = multi.Read<int>().Single();
                return user;
            }

            // IPredicateGroup predicate = DapperLinqBuilder<User>.FromExpression(expression);
            // IList<ISort> sort = SortConvert(sortList);
            //// Conn.Query<>()

            // ExpressionToSql sql = new ExpressionToSql();

            // Expression<Func<User, bool>> aaa = u => u.Id == 1 && u.Name.DB_NotLike("123");

            // Expression<Func<User, bool>> bbb = u => u.Id == 1 && u.Name.DB_Like("aa");

            // var ss = LambdaToSqlHelper.GetWhereSql(aaa); ;
            // var dsds = sql.GetSql(bbb); ;
            // var ddd = sql.GetSql(expression);
            //throw new NotImplementedException();
        }




    }
}