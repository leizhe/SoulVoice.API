using System.Collections.Generic;
using System.Linq;
using Dapper;
using SV.Entity.Query;
using SV.Repository.Base;
using SV.Repository.Core.Query;

namespace SV.Repository.Query
{
    public class PermissionQueryRepository : DapperRepositoryBase<Permission>, IPermissionQueryRepository
    {
	    public List<Permission> GetPermissionsByRoleIds(List<long> roleIds)
	    {
		    var where = $"WHERE r.RoleId IN({ GetIdsString(roleIds)})";
		    var sql = BaseSql() + where;
		    return Conn.Query<Permission>(sql).ToList();
		}

	    private string BaseSql()
	    {
			return @"SELECT p.Access,p.AccessValue FROM rolepermission r 
							LEFT JOIN permission p ON r.PermissionId = p.Id ";
		}

	}
}
