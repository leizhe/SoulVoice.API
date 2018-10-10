using System.Collections.Generic;
using SV.Entity.Query;

namespace SV.Repository.Core.Query
{
    public interface IPermissionQueryRepository : IDapperQueryRepository<Permission>
    {
	    List<Permission> GetPermissionsByRoleIds(List<long> ids);
    }
}