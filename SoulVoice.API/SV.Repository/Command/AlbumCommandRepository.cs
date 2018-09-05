using SV.Entity.Command;
using SV.Repository.Base;
using SV.Repository.Core.Command;

namespace SV.Repository.Command
{
    public class AlbumCommandRepository : EntityFrameworkRepositoryBase<Album>, IAlbumCommandRepository
	{
        
    }
}