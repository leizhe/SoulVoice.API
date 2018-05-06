using System;

namespace SV.Entity.Auditing
{
    public interface IHasCreationTime
    {
        DateTime CreationTime { get; set; } 
    }
}