﻿using System;
using System.Collections.Generic;

namespace SV.Application.Dtos
{
    public class UserDto :BaseEntityDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Password { get; set; }

        public string IdCard { get; set; }

        public decimal Money { get; set; }

        public string Alipay { get; set; }

        public string WeChat { get; set; }

        public string ApplePay { get; set; }

        public DateTime CreationTime { get; set; }

        public int State { get; set; }
        
        public List<RoleDto> Roles { get; set; }
        public int TotalRole { get; set; }
    }
}