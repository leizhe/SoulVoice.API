using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SV.Application.Dtos;
using SV.Application.Exceptions;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;

namespace SV.API.Controllers
{
	[Produces("application/json")]
	public class AlbumController : Controller
	{
		private readonly IAlbumService _albumService;
		public AlbumController(IAlbumService albumService)
		{
			_albumService = albumService;
		}

		[HttpGet]
		[Route("api/Album/Page")]
		public GetResults<AlbumDto> GetPage(long classifyId, PageInput input)
		{
			return _albumService.GetAlbumPageByClassifyId(classifyId, input);
		}

	}

}
