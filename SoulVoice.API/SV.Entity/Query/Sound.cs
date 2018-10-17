using System;
using DapperExtensions.Mapper;
using SV.Entity.Auditing;

namespace SV.Entity.Query
{
	public sealed class Sound : BaseEntity, ICreationAudited,IEntity
	{
		public long AlbumId { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
		public long PlayCount { get; set; }
		public double Price { get; set; }
		public long? CreatorUserId { get; set; }
		public DateTime CreationTime { get; set; }
		public Album Album { get; set; }
		[Serializable]
		public sealed class SoundOrmMapper : ClassMapper<Sound>
		{
			public SoundOrmMapper()
			{
				Table("Sound");
				Map(f => f.Album).Ignore();
				AutoMap();
			}
		}
	}
}
