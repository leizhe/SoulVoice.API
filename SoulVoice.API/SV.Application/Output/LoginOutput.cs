using System.Collections.Generic;
using SV.Application.Dtos;

namespace SV.Application.Output
{
    public class LoginOutput
    {
		public long UserId { get; set; }
        public string User { get; set; }
	    public string Role { get; set; }
		public List<MenuDto> Menus { get; set; }
		public object Token { get; set; }
    }
}
