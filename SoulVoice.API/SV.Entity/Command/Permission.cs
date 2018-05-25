using System.Collections.Generic;

namespace SV.Entity.Command
{
    public sealed class Permission : BaseEntity
    {
        public int Access { get; set; }
        public int AccessValue { get; set; }
        
    }
}