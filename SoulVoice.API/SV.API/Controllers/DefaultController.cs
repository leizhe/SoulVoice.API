using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SV.Application.Dtos;
using SV.Application.Input;
using SV.Application.Output;
using SV.Application.ServiceContract;
using SV.API.Jwt;

namespace SV.API.Controllers
{
	public class DefaultController : BaseController
	{
	    private readonly JwtSettings _jwtSettings;
		private readonly IAccountService _accountService;
        public DefaultController(IOptions<JwtSettings> jwtSettingsAccesser, IAccountService accountService)
        {
	        _jwtSettings = jwtSettingsAccesser.Value;
			_accountService = accountService;
        }

  //      [HttpGet]
  //      public GetResults<MenuDto> DefaultMenu()
  //      {
		//	var result = _accountService.Login(nameOrEmail, passWord);
	 //       result.Data.Token = JwtAuthorize.IssueToken(_jwtSettings,result.Data);
	 //       return result;
  //      }

  //      [HttpGet]
  //      public GetResults<AlbumDto> DefaultAlbum()
  //      {
	 //       CheckModelState();
	 //       return _accountService.Register(input);
		//}

    }
}
