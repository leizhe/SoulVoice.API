using SV.Entity;
using SV.Repository.Base;
using SV.Repository.Core.Command;

namespace SV.Repository.Command
{
    public class UserCommandRepository : EntityFrameworkRepositoryBase<User>, IUserCommandRepository
    {
        
    }
}