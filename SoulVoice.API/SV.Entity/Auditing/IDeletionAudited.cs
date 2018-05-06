﻿using System;

namespace SV.Entity.Auditing
{
    public interface IDeletionAudited : ISoftDelete
    {

        long? DeleterUserId { get; set; }

       
        DateTime? DeletionTime { get; set; } 
    }
}