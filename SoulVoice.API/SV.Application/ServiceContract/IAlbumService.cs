using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;

namespace SV.Application.ServiceContract
{
    public interface IAlbumService
    {
	    GetResult<AlbumDto> GetAlbum(long albumId);
		GetResults<AlbumDto> GetAlbumPageByClassifyId(long classifyId, PageInput input);
	    GetResults<AlbumDto> GetAlbumRankByClassifyId(long classifyId, PageInput input);
		GetResults<AlbumDto> FilterAlbum(PageFilterInput input);
	    CreateResult<long> AddAlbum(AlbumInput input);
	    UpdateResult UpdateAlbum(AlbumInput input);

	}
}