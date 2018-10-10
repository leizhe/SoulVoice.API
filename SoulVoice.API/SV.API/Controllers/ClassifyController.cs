using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV.Application.Dtos;
using SV.Application.Output;
using SV.Application.ServiceContract;

namespace SV.API.Controllers
{
	[Authorize]
	public class ClassifyController : BaseController
	{
		private readonly IClassifyService _classifyService;
		public ClassifyController(IClassifyService classifyService)
		{
			_classifyService = classifyService;
		}

		[HttpGet]
		public GetResults<ClassifyDto> All()
		{
			return _classifyService.GetAll();
		}

	}
}
