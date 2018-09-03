﻿using System.Collections.Generic;
using SV.Entity;
using SV.Entity.Command;

namespace SV.Application.Dtos
{
	public class ClassifyDto : BaseEntityDto
	{
		public string Name { get; set; }
		public string Memo { get; set; }
	}
}
