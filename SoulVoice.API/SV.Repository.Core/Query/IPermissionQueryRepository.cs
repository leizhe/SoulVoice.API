using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SV.Entity.Query;

namespace SV.Repository.Core.Query
{
    public interface IPermissionQueryRepository : IDapperQueryRepository<Permission>
    {
	    List<Permission> GetPermissionsByRoleIds(List<long> ids);
    }
}