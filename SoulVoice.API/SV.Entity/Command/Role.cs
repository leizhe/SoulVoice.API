using System;
using System.Collections.Generic;
using SV.Entity.Auditing;

namespace SV.Entity.Command
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Memo { get; set; }

    }
}