using System;
using DapperExtensions.Mapper;

namespace SV.Entity
{
    public sealed class RolePermission : BaseEntity
    {
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
        public Role Role { get; set; }
        public Permission Permission { get; set; }

        [Serializable]
        public sealed class RolePermissionOrmMapper : ClassMapper<RolePermission>
        {
            public RolePermissionOrmMapper()
            {
                Table("RolePermission");
                Map(f => f.Permission).Ignore();
                Map(f => f.Role).Ignore();
                Map(f => f.Id).Key(KeyType.Identity);
                AutoMap();
            }
        }
    }
}