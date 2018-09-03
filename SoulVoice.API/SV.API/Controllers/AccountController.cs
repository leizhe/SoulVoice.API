using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SV.Application.Exceptions;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;

namespace SV.API.Controllers
{
	[Produces("application/json")]
	public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("api/account/Login")]
        public GetResult<LoginOutput> Login(string nameOrEmail, string passWord)
        {
            return _accountService.Login(nameOrEmail,passWord);
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
