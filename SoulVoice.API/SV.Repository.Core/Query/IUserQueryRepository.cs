using System.Collections.Generic;
using SV.Entity.Query;

namespace SV.Repository.Core.Query
{
    public interface IUserQueryRepository : IDapperQueryRepository<User>
    {
        //List<User> GetAll();
        //User GetById();
    }
}