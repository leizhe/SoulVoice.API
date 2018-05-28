using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SV.Entity.Query;

namespace SV.Repository.Core.Query
{
    public interface IAccountQueryRepository : IDapperQueryRepository<User>
    {
        List<Menu> GetPermissions(List<long> roleIds);
    }
}