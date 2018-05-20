using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SV.Entity.Command
{
    public sealed class Role : BaseEntity
    {
        public string Name { get; set; }

        [DisplayFormat(NullDisplayText = "No Memo")]
        public string Memo { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        //public Role()
        //{
        //    RolePermissions = new HashSet<RolePermission>();
        //    UserRoles = new HashSet<UserRole>();
        //}
    }
}