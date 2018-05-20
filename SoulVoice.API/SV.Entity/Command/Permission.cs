using System.Collections.Generic;

namespace SV.Entity.Command
{
    public sealed class Permission : BaseEntity
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }

        //public Permission()
        //{
        //    RolePermissions = new HashSet<RolePermission>();
        //}
    }
}