using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Application.Dtos
{
    public class RoleDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Memo { get; set; }
    }
}
