using System;
using System.Collections.Generic;
using DapperExtensions.Mapper;
using SV.Entity.Auditing;

namespace SV.Entity.Query
{
	public sealed class Album : BaseEntity, ICreationAudited, IEntity
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
		public Classify Classify { get; set; }
		public ICollection<Sound> Sounds { get; set; }
		public User CreatorUser { get; set; }
		public Album()
		{
			Sounds = new HashSet<Sound>();
		}
		[Serializable]
		public sealed class AlbumOrmMapper : ClassMapper<Album>
		{
			public AlbumOrmMapper()
			{
				Table("Album");
				Map(f => f.Classify).Ignore();
				Map(f => f.Sounds).Ignore();
				Map(f => f.CreatorUser).Ignore();
				Map(f => f.Id).Key(KeyType.Identity);
				AutoMap();
			}
		}
	}
}
