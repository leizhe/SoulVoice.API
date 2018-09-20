using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SV.Application.Exceptions;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;
using SV.API.Jwt;

namespace SV.API.Controllers
{
	[Produces("application/json")]
	public class AccountController : Controller
    {
	    private readonly JwtSettings _jwtSettings;
		private readonly IAccountService _accountService;
        public AccountController(IOptions<JwtSettings> jwtSettingsAccesser, IAccountService accountService)
        {
	        _jwtSettings = jwtSettingsAccesser.Value;
			_accountService = accountService;
        }

        [HttpGet]
        [Route("api/account/Login")]
        public GetResult<LoginOutput> Login(string nameOrEmail, string passWord)
        {
			var result = _accountService.Login(nameOrEmail, passWord);
	        result.Data.Token = JwtAuthorize.GenToken(_jwtSettings,result.Data);
	        return result;
        }

        [HttpPost]
        [Route("api/account/Register")]
        public CreateResult<long> Register([FromBody]RegisterInput input)
        {
            if (ModelState.IsValid)
            {
                return _accountService.Register(input);
            }
            throw new ModelStateErrorException();

        }

    }
}
