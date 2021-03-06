﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;
using SV.API.Jwt;

namespace SV.API.Controllers
{
	[Authorize]
	public class AlbumController : BaseController
	{
		private readonly IAlbumService _albumService;
		public AlbumController(IAlbumService albumService)
		{
			_albumService = albumService;
		}

		[HttpGet]
		public GetResults<AlbumDto> Page(long classifyId, PageInput input)
		{
			var user= JwtAuthorize.AuthUser(this);
			return _albumService.GetAlbumPageByClassifyId(classifyId, input);
		}

		[HttpGet]
		public GetResults<AlbumDto> Rank(long classifyId, PageInput input)
		{
			return _albumService.GetAlbumRankByClassifyId(classifyId, input);
		}

		[HttpGet]
		public GetResults<AlbumDto> Filter(PageFilterInput input)
		{
			CheckModelState();
			return _albumService.FilterAlbum(input);
		}

		[HttpGet]
		public GetResult<AlbumDto> Get(long albumId)
		{
			return _albumService.GetAlbum(albumId);
		}

		[HttpPost]
		public CreateResult<long> Add(AlbumInput input)
		{
			CheckModelState();
			return _albumService.AddAlbum(input);
		}

		[HttpPut]
		public UpdateResult Update(AlbumInput input)
		{
			CheckModelState();
			return _albumService.UpdateAlbum(input);
		}
	}

}
