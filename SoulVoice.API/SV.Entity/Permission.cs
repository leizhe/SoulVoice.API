using System;
using DapperExtensions.Mapper;
using SV.Entity.Auditing;

namespace SV.Entity
{
    public sealed class Permission : BaseEntity, IEntity
    {
        public long Access { get; set; }
        public long AccessValue { get; set; }

        [Serializable]
        public sealed class PermissionOrmMapper : ClassMapper<Permission>
        {
            public PermissionOrmMapper()
            {
                Table("Permission");
                AutoMap();
            }
        }
    }
}