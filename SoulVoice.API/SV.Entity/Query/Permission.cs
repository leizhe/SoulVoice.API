using System;
using System.Collections.Generic;
using DapperExtensions.Mapper;

namespace SV.Entity.Query
{
    public sealed class Permission : BaseEntity
    {
        public int Access { get; set; }
        public int AccessValue { get; set; }

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