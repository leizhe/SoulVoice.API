﻿using System.Linq;
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
	public class ClassifyController : BaseController
	{
		private readonly IClassifyService _classifyService;
		public ClassifyController(IClassifyService classifyService)
		{
			_classifyService = classifyService;
		}

		[HttpGet]
		
		//[Route("api/Classify/All")]
		public GetResults<ClassifyDto> All()
		{
			return _classifyService.GetAll();
		}

	}
}
