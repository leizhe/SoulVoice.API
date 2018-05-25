using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SV.Entity.Query;

namespace SV.Repository.Core.Query
{
    public interface IUserQueryRepository : IDapperQueryRepository<User>
    {
        User Get(Expression<Func<User, bool>> expression);
        User GetById(long userId);
        List<User> GetAll();
        List<User> GetPage(int pageNum, int pageSize, out long outTotal,Expression<Func<User, bool>> expression = null, object sortList = null);
    }
}