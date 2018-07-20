using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SV.Application.Input
{
    public class RegisterInput
    {
        public string Name { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email format error.")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
