using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using Dapper;
using SV.Common.Extensions;
using SV.Entity.Query;
using SV.Repository.Base;
using SV.Repository.Core.Query;

namespace SV.Repository.Query
{
    public class UserQueryRepository : DapperRepositoryBase<User>, IUserQueryRepository
    {
        
        public User GetById(long userId)
        {
            var where = $@"WHERE u.Id={userId}";
            return GetSingleByWhere(where);
        }

        public User GetByAccountAndPassword(string nameOrEmail, string passWord)
        {
            var where = $@"WHERE (u.Name='{nameOrEmail}' or u.Email='{nameOrEmail}') and u.Password='{passWord}'";
            return GetSingleByWhere(where);
        }

        public List<User> GetAll()
        {
            return GetListByWhere(null);
        }

        public List<User> GetPage(int pageNum, int pageSize, out long outTotal, string where = null, object sortList = null)
        {
            var baseSql = GetBaseSql();

            var commandSql = GetPageSql(baseSql,"User","u", pageNum, pageSize);

            var lookup = new Dictionary<long, User>();
            using (var multi = Conn.QueryMultiple(commandSql))
            {
                outTotal = multi.Read<int>().Single();
                multi.Read(FillDic(lookup), splitOn: "Id");
                return lookup.Values.ToList();
            }
        }

        private string GetBaseSql()
        {
            return @"SELECT * FROM User AS u 
                        LEFT JOIN UserRole AS ur ON u.Id=ur.UserId
                        LEFT JOIN Role AS r ON ur.RoleId=r.Id ";
        }

        private List<User> GetListByWhere(string where)
        {

            var sql = GetBaseSql();
            if (!string.IsNullOrEmpty(where))
            {
                sql += where;
            }
            return GetUserDictionary(sql).ToList();
        }
        
        private User GetSingleByWhere(string where)
        {
            var sql = GetBaseSql();
            if (!string.IsNullOrEmpty(where))
            {
                sql += where;
            }
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