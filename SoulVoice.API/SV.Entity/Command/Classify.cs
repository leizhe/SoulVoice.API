using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Entity.Command
{
	public sealed class Classify : BaseEntity
	{
		public string Name { get; set; }
		public string Memo { get; set; }
		public ICollection<Album> Albums { get; set; }
	}
}
