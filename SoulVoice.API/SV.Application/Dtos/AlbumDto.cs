using System;
using System.Collections.Generic;
using SV.Entity.Command;

namespace SV.Application.Dtos
{
	public sealed class AlbumDto : BaseEntityDto
	{
		public long ClassifyId { get; set; }
		public string Name { get; set; }
		public string Memo { get; set; }
		public string Pic { get; set; }
		public long PlayCount { get; set; }
		public long SubCount { get; set; }
		public long BuyCount { get; set; }
		public double Price { get; set; }
		public DateTime LastUpdate { get; set; }
		public int Status { get; set; }
		public long? CreatorUserId { get; set; }
		public DateTime CreationTime { get; set; }
		public string CreatorUserName { get; set; }
		public ICollection<SoundDto> Sounds { get; set; }
	}
}
