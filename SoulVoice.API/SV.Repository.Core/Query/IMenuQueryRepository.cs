using System.Collections.Generic;
using SV.Entity;

namespace SV.Repository.Core.Query
{
    public interface IMenuQueryRepository : IDapperQueryRepository<Menu>
    {
	    List<Menu> GetDefault();
		List<Menu> GetAll();
	    List<Menu> GetByIds(List<long> menuIds,List<long> actionIds);
	}
}