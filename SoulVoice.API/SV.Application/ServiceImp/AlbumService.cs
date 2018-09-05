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
    public class AlbumService : BaseService, IAlbumService
	{
        private readonly IMapper _mapper;
		private readonly IAlbumQueryRepository _albumQuery;
		private readonly IAlbumCommandRepository _albumCommand;
		

		public AlbumService(IMapper mapper,
			IAlbumQueryRepository albumQueryRepository,
			IAlbumCommandRepository albumCommandRepository)
        {
            _mapper = mapper;
	        _albumQuery = albumQueryRepository;
			_albumCommand = albumCommandRepository;

		}

		public CreateResult<long> AddAlbum(AlbumInput input)
		{
			var result = GetDefault<CreateResult<long>>();
			var album = _mapper.Map<Album>(input);
			album.CreationTime=DateTime.UtcNow;
			album.LastUpdate = DateTime.UtcNow;
			album.PlayCount = 0;
			album.BuyCount = 0;
			album.SubCount = 0;
			_albumCommand.Add(album);
			_albumCommand.Commit();
			result.Id = album.Id;
			result.IsCreated = true;
			return result;
		}

		public UpdateResult UpdateAlbum(AlbumInput input)
		{
			var result = GetDefault<UpdateResult>();
			var existAlbum = _albumQuery.FindSingle(u => u.Id == input.Id);
			if (existAlbum == null)
			{
				result.Message = "The Album not exist";
				result.StateCode = (int)StatusCode.AlbumNotExist;
				return result;
			}
			if (IsHasSameName(input.Name, existAlbum.Id))
			{
				result.Message = "The AlbumName has exist";
				result.StateCode = (int)StatusCode.AlbumNameHasExist;
				return result;
			}
			_albumCommand.Update(p => p.Id == existAlbum.Id, u => new Album()
			{
				ClassifyId = input.ClassifyId,
				Pic =input.Pic,
				Name = input.Name,
				Memo = input.Memo,
				Price = input.Price ?? 0
			});
			result.IsSaved = true;
			return result;
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
		private bool IsHasSameName(string name,long id)
		{
			return !string.IsNullOrWhiteSpace(name) && _albumQuery.Exists(a => a.Name == name && a.Id==id);
		}

		public GetResult<AlbumDto> GetAlbum(long albumId)
		{
			var result = GetDefault<GetResult<AlbumDto>>();
			var model = _albumQuery.GetById(albumId);
			if (model == null)
			{
				result.Message = "The Album not exist";
				result.StateCode = (int)StatusCode.AlbumNotExist;
				return result;
			}
			result.Data = _mapper.Map<AlbumDto>(model);
			return result;
		}
	}
}
