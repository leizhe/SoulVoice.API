using System;
using System.Collections.Generic;
using System.Text;
using SV.Entity.Query;
using SV.Repository.Base;
using SV.Repository.Core.Query;

namespace SV.Repository.Query
{
    public class AccountQueryRepository : DapperRepositoryBase<User>, IAccountQueryRepository
    {
        public List<Menu> GetPermissions(long roleId)
        {
            throw new NotImplementedException();
        }
    }
}
