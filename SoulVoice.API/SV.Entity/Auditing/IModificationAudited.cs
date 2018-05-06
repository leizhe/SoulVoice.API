using System;

namespace SV.Entity.Auditing
{
    public interface IModificationAudited
    {

        DateTime? LastModificationTime { get; set; }


        long? LastModifierUserId { get; set; }
    }
}