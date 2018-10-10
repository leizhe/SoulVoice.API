using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Application.Dtos
{
    public class MenuDto
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public List<ActionDto> Actions { get; set; }
    }
}
