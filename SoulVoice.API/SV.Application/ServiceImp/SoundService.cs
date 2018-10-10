using System;
using System.Collections.Generic;
using AutoMapper;
using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;
using SV.Application.Status;
using SV.Entity.Command;
using SV.Repository.Core.Command;
using SV.Repository.Core.Query;

namespace SV.Application.ServiceImp
{
    public class SoundService : BaseService, ISoundService
	{
        private readonly IMapper _mapper;
		private readonly ISoundQueryRepository _soundQuery;
		//private readonly IAlbumCommandRepository _albumCommand;


		public SoundService(IMapper mapper, 
			ISoundQueryRepository soundQueryRepository)
        {
            _mapper = mapper;
	        _soundQuery = soundQueryRepository;

        }

		public GetResults<SoundDto> GetSoundPageByAlbumId(long albumId, PageInput input)
		{
			var result = GetDefault<GetResults<SoundDto>>();
			var sounds = _soundQuery.Page(input.Current, input.Size, out var pageCount, sound => sound.AlbumId == albumId, new {Id = true});
			result.Data = _mapper.Map<List<SoundDto>>(sounds);
			result.Total = pageCount;
			return result;
		}

		//public GetResults<AlbumDto> GetAlbumPageByClassifyId(long classifyId, PageInput input)
		//{
		//	var result = GetDefault<GetResults<AlbumDto>>();
		//	var classifies = _albumQuery.GetPageByClassifyId(input.Current, input.Size, out var pageCount, classifyId);
		//	result.Data = _mapper.Map<List<AlbumDto>>(classifies);
		//	result.Total = pageCount;
		//	return result;
		//}

		//public GetResults<AlbumDto> GetAlbumRankByClassifyId(long classifyId, PageInput input)
		//{
		//	var result = GetDefault<GetResults<AlbumDto>>();
		//	var classifies = _albumQuery.Page(input.Current, input.Size, out var pageCount, album => album.ClassifyId == classifyId, new { SubCount = true, PlayCount = true, BuyCount = true });
		//	result.Data = _mapper.Map<List<AlbumDto>>(classifies);
		//	result.Total = pageCount;
		//	return result;
		//}

	}
}
