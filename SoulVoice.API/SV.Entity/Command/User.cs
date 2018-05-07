using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SV.Entity.Auditing;

namespace SV.Entity.Command
{
    public class User : BaseEntity, IHasCreationTime
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(50)]
        public string IdCard { get; set; }
        
        public decimal Money { get; set; }
        
        public string Alipay { get; set; }
        public string WeChat { get; set; }
        
        public string ApplePay { get; set; }

        public DateTime CreationTime { get; set; }

        public int State { get; set; }

        public Role Role { get; set; }

       


    }
}