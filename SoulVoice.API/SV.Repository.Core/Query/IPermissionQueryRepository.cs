using System.Collections.Generic;
using SV.Entity;

namespace SV.Repository.Core.Query
{
    public interface IPermissionQueryRepository : IDapperQueryRepository<Permission>
    {
	    List<Permission> GetPermissionsByRoleIds(List<long> ids);
    }
}