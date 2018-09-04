using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

		public GetResults<AlbumDto> FilterAlbum(PageFilterInput input)
		{
			var result = GetDefault<GetResults<AlbumDto>>();
			var classifies = _albumQuery.FilterPage(input.Current, input.Size, out var pageCount,input.Filter);
			result.Data = _mapper.Map<List<AlbumDto>>(classifies);
			result.Total = pageCount;
			return result;
		}

		public GetResults<AlbumDto> GetAlbumPageByClassifyId(long classifyId, PageInput input)
		{
			var result = GetDefault<GetResults<AlbumDto>>();
			var classifies = _albumQuery.GetPageByClassifyId(input.Current, input.Size, out var pageCount, classifyId);
			result.Data = _mapper.Map<List<AlbumDto>>(classifies);
			result.Total = pageCount;
			return result;
		}

		public GetResults<AlbumDto> GetAlbumRankByClassifyId(long classifyId, PageInput input)
		{
			var result = GetDefault<GetResults<AlbumDto>>();
			var classifies = _albumQuery.Page(input.Current, input.Size, out var pageCount, album => album.ClassifyId == classifyId, new { SubCount = true, PlayCount = true, BuyCount = true });
			result.Data = _mapper.Map<List<AlbumDto>>(classifies);
			result.Total = pageCount;
			return result;
		}
		
	}
}
