﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SV.API.Jwt
{
    public class AuthUser
    {
	    public long UserId { get; set; }
	    public string User { get; set; }
	    public string Role { get; set; }
	}
}
