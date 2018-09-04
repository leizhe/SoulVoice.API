using System.Collections.Generic;
using AutoMapper;
using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;
using SV.Repository.Core.Query;

namespace SV.Application.ServiceImp
{
    public class AlbumService : BaseService, IAlbumService
	{
        private readonly IMapper _mapper;
		private readonly IAlbumQueryRepository _albumQuery;
		

		public AlbumService(IMapper mapper,
			IAlbumQueryRepository albumQueryRepository
		   )
        {
            _mapper = mapper;
	        _albumQuery = albumQueryRepository;

        }

		public GetResults<AlbumDto> GetAlbumPageByClassifyId(long classifyId, PageInput input)
		{
			var result = GetDefault<GetResults<AlbumDto>>();
			var classifies = _albumQuery.Page(input.Current, input.Size, out var pageByExpOrderCount, album => album.ClassifyId == classifyId, new { LastUpdate = true });
			result.Total = pageByExpOrderCount;
			result.Data = _mapper.Map<List<AlbumDto>>(classifies);
			return result;
		}
	}
}
