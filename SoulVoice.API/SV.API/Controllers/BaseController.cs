using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV.Application.Exceptions;

namespace SV.API.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]/[action]")]
	public class BaseController : Controller
	{
		protected void CheckModelState()
		{
			//protected void CheckModelState<T>(string routeUrl, T input)
			if (!ModelState.IsValid)
				throw new ModelStateErrorException();
		}
	}
}
