using System;
using System.Collections.Generic;
using DapperExtensions.Mapper;
using SV.Entity.Command;

namespace SV.Entity.Query
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Memo { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        public Role()
        {
            RolePermissions = new HashSet<RolePermission>();
            UserRoles = new HashSet<UserRole>();
        }
        [Serializable]
        public sealed class RoleOrmMapper : ClassMapper<Role>
        {
            public RoleOrmMapper()
            {
                Table("Role");
                Map(f => f.UserRoles).Ignore();
                Map(f => f.RolePermissions).Ignore();
                AutoMap();
            }
        }
    }
   
}