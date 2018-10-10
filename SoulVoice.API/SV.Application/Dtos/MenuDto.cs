using System.Collections.Generic;

namespace SV.Application.Dtos
{
    public class MenuDto
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public List<ActionDto> Actions { get; set; }
    }
}
