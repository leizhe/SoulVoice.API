using System;
using System.Collections.Generic;
using System.Text;
using SV.Application.Dtos;

namespace SV.Application.Output
{
    public class LoginOutput: OutputBase
    {
        public string UserName { get; set; }
        public List<MenuDto> Menus { get; set; }
    }
}
