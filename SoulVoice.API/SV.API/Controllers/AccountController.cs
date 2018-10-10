using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;
using SV.API.Jwt;

namespace SV.API.Controllers
{
	public class AccountController : BaseController
	{
	    private readonly JwtSettings _jwtSettings;
		private readonly IAccountService _accountService;
        public AccountController(IOptions<JwtSettings> jwtSettingsAccesser, IAccountService accountService)
        {
	        _jwtSettings = jwtSettingsAccesser.Value;
			_accountService = accountService;
        }

        [HttpGet]
        public GetResult<LoginOutput> Login(string nameOrEmail, string passWord)
        {
			var result = _accountService.Login(nameOrEmail, passWord);
	        result.Data.Token = JwtAuthorize.IssueToken(_jwtSettings,result.Data);
	        return result;
        }

        [HttpPost]
        public CreateResult<long> Register([FromBody]RegisterInput input)
        {
	        CheckModelState();
	        return _accountService.Register(input);
		}

    }
}
