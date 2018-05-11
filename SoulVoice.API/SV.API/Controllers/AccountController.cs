using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public string Login(string nameOrEmail,string passWord)
        {
            return nameOrEmail+ passWord;
        }
        
        [HttpPost]
        [Route("api/account/Register")]
        public void Register([FromBody]string value)
        {
        }

    }
}
