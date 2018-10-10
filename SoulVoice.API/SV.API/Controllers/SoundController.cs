using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV.Application.Dtos;
using SV.Application.Exceptions;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;

namespace SV.API.Controllers
{
	[Authorize]
	public class SoundController : BaseController
	{
		private readonly ISoundService _soundService;
		public SoundController(ISoundService soundService)
		{
			_soundService = soundService;
		}

		[HttpGet]
		public GetResults<SoundDto> Page(long albumId, PageInput input)
		{
			return _soundService.GetSoundPageByAlbumId(albumId, input);
		}

	}
}
