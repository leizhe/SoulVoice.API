using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using SV.Entity.Query;
using SV.Repository.Base;
using SV.Repository.Core.Query;

namespace SV.Repository.Query
{
    public class UserQueryRepository : DapperRepositoryBase<User>, IUserQueryRepository
    {

        public User Get(Expression<Func<User, bool>> expression)
        {
            // IPredicateGroup predicate = DapperLinqBuilder<User>.FromExpression(expression);
            // IList<ISort> sort = SortConvert(sortList);
            //// Conn.Query<>()

            // ExpressionToSql sql = new ExpressionToSql();

            //Expression<Func<User, bool>> aaa = u => u.Id == 1 && u.Name.DB_NotLike("123");

            //Expression<Func<User, bool>> bbb = u => u.Id == 1 && u.Name.DB_Like("aa");

            // var ss = LambdaToSqlHelper.GetWhereSql(aaa); ;
            // var dsds = sql.GetSql(bbb); ;
            // var ddd = sql.GetSql(expression);
            throw new NotImplementedException();
        }

        public User GetById(long userId)
        {
            var sql = $@"SELECT * FROM User AS u 
                        LEFT JOIN UserRole AS ur ON u.Id=ur.UserId
                        LEFT JOIN Role AS r ON ur.RoleId=r.Id
                        WHERE u.Id={userId}";
            return GetSingleBySql(sql);
        }

        public List<User> GetAll()
        {
            var sql = @"SELECT * FROM User AS u 
                        LEFT JOIN UserRole AS ur ON u.Id=ur.UserId
                        LEFT JOIN Role AS r ON ur.RoleId=r.Id";

            return GetListBySql(sql);
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
        }
        
        private List<User> GetListBySql(string sql)
        {
            return GetUserDictionary(sql).ToList();
        }

        private User GetSingleBySql(string sql)
        {
            return GetUserDictionary(sql).FirstOrDefault();
        }

        private Dictionary<long, User>.ValueCollection GetUserDictionary(string sql)
        {
            var lookup = new Dictionary<long, User>();
            Conn.Query(sql, FillDic(lookup));
            return lookup.Values;
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