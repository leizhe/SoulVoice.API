using Microsoft.AspNetCore.Mvc;
using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;

namespace SV.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("api/user/GetAllUsers")]
        public OutputBase GetAllUsers()
        {
            PageInput input = new PageInput() { Current = 1, Size = 10 };
            return _userService.GetUsers(input);
        }

        [HttpGet]
        [Route("api/user/GetUsers")]
        public OutputBase GetUsers(PageInput input)
        {
            return _userService.GetUsers(input);
        }

        [HttpGet]
        [Route("api/user/Get/{id}")]
        public GetResult<UserDto> Get(int id)
        {
            return _userService.GetUser(id);
        }

        [HttpPut]
        [Route("api/user/Put/{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        [Route("api/user/Delete/{id}")]
        public void Delete(int id)
        {
        }
    }
}
