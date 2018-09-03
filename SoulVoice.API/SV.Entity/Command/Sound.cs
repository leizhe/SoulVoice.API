using System;
using System.Collections.Generic;
using System.Text;
using SV.Entity.Auditing;

namespace SV.Entity.Command
{
	public sealed class Sound : BaseEntity, ICreationAudited
	{
		public long AlbumId { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public long PlayCount { get; set; }
		public double Price { get; set; }
		public long? CreatorUserId { get; set; }
		public DateTime CreationTime { get; set; }
		public Album Album { get; set; }
	}
}
