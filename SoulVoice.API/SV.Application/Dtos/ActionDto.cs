﻿using System.Collections.Generic;
namespace ED.Application.Dtos
{
    public class ActionDto : BaseEntityDto
    {
        public List<BaseEntityDto> Roles { get; set; }

        public List<BaseEntityDto> Users { get; set; }
    }
}