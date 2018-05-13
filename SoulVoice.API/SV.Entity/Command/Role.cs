﻿using System.Collections.Generic;

namespace SV.Entity.Command
{
    public sealed class Role : BaseEntity
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
    }
}