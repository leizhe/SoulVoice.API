using System;
using DapperExtensions.Mapper;

namespace SV.Entity.Query
{
    public sealed  class UserRole : BaseEntity
    {
        public long RoleId { get; set; }
        public long UserId { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
        [Serializable]
        public sealed class UserRoleOrmMapper : ClassMapper<UserRole>
        {
            public UserRoleOrmMapper()
            {
                Table("UserRole");
                Map(f => f.User).Ignore();
                Map(f => f.Role).Ignore();
                Map(f => f.Id).Key(KeyType.Identity);
                AutoMap();
            }
        }
    }
}
