using Microsoft.AspNetCore.Mvc;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;

namespace SV.API.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("api/account/Login")]
        public string Login(string nameOrEmail, string passWord)
        {
            return nameOrEmail + passWord;
        }

        [HttpPost]
        [Route("api/account/Register")]
        public CreateResult<long> Register([FromBody]RegisterInput input)
        {
            return _userService.Register(input);
        }

    }
}
