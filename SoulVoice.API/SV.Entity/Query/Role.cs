using System;
using DapperExtensions.Mapper;

namespace SV.Entity.Query
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Memo { get; set; }
    }
    [Serializable]
    public sealed class RoleOrmMapper : ClassMapper<Role>
    {
        public RoleOrmMapper()
        {
            base.Table("Role");
            AutoMap();
        }
    }
}