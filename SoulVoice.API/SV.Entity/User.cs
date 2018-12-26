using System;
using System.Collections.Generic;
using DapperExtensions.Mapper;
using SV.Entity.Auditing;

namespace SV.Entity
{
    [Serializable]
    public class User : BaseEntity, IHasCreationTime, IEntity
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

        public ICollection<UserRole> UserRoles { get; set; }

        public User()
        {
            UserRoles = new List<UserRole>();
        }

        [Serializable]
        public sealed class UserOrmMapper : ClassMapper<User>
        {
            public UserOrmMapper()
            {
                Table("User");
                Map(f => f.UserRoles).Ignore();
                Map(f => f.Id).Key(KeyType.Identity);
                AutoMap();
            }
        }
    }
    
}