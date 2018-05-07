﻿using System.Collections.Generic;
using System.Linq;
using Dapper;
using SV.Entity.Query;
using SV.Repository.Base;
using SV.Repository.Core.Query;

namespace SV.Repository.Query
{
    public class UserQueryRepository : DapperRepositoryBase<User>, IUserQueryRepository
    {

        public UserQueryRepository(DapperContext context) : base(context)
        {
        }

        public List<User> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<User> GetAllQueryable()
        {
            throw new System.NotImplementedException();
        }

        //public List<User> GetAll()
        //{
        //    using (var db = Context.GetConnection())
        //    {
        //        var sql = @"SELECT * FROM [User] AS u 
        //                LEFT JOIN [UserRole] AS ur ON u.Id=ur.UserId
        //                LEFT JOIN [Role] AS r ON ur.RoleId=r.Id";
        //        var lookup = new Dictionary<long, User>();
        //        db.Query<User, UserRole, Role, User>(sql, (u, ur, r) =>
        //            {
        //                if (!lookup.TryGetValue(u.Id, out var tmp))
        //                {
        //                    tmp = u;
        //                    lookup.Add(u.Id, tmp);
        //                }

        //                var tmpUr = tmp.UserRoles.FirstOrDefault(p => p.Id == ur.Id);
        //                if (tmpUr == null)
        //                {
        //                    tmpUr = ur;
        //                    tmp.UserRoles.Add(tmpUr);
        //                    tmpUr.Role = r;
        //                }

        //                return u;
        //            },
        //            splitOn: "Id");
        //        return lookup.Values.ToList();
        //    }

        //}

        //public IQueryable<User> GetAllQueryable()
        //{
        //    using (var db = Context.GetConnection())
        //    {
        //        var sql = @"SELECT * FROM [User] AS u 
        //                LEFT JOIN [UserRole] AS ur ON u.Id=ur.UserId
        //                LEFT JOIN [Role] AS r ON ur.RoleId=r.Id";
        //        //var lookup = new Dictionary<long, User>();
        //        var lst = new List<User>();
        //        var q = db.Query<User, UserRole, Role, User>(sql, (u, ur, r) =>
        //             {
        //                 var tmp = lst.FirstOrDefault(p => p.Id == u.Id);
        //                 if (tmp == null)
        //                 {
        //                     tmp = u;
        //                 }

        //                 var tmpUr = tmp.UserRoles.FirstOrDefault(p => p.Id == ur.Id);
        //                 if (tmpUr == null)
        //                 {
        //                     tmpUr = ur;
        //                     tmp.UserRoles.Add(tmpUr);
        //                     tmpUr.Role = r;
        //                 }
        //                 return u;
        //             },
        //            splitOn: "Id").AsQueryable();
        //        return q;
        //        // return lst.AsQueryable();
        //    }

        //}

        public User GetById()
        {
            throw new System.NotImplementedException();
        }
    }
}