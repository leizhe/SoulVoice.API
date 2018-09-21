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
	
	[Produces("application/json")]
	public class ClassifyController : Controller
	{
		private readonly IClassifyService _classifyService;
		public ClassifyController(IClassifyService classifyService)
		{
			_classifyService = classifyService;
		}

		[HttpGet]
		[Authorize]
		[Route("api/Classify/All")]
		public GetResults<ClassifyDto> GetAll()
		{
			return _classifyService.GetAll();
		}

	}
}
