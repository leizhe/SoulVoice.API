using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SV.Entity.Auditing;

namespace SV.Entity.Command
{
    public sealed class User : BaseEntity, IHasCreationTime
    {
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "用户名长度不能超过50.")]
        [Display(Name = "用户名")]
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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationTime { get; set; }

        public int State { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        //public User()
        //{
        //    UserRoles = new List<UserRole>();
        //}




    }
}