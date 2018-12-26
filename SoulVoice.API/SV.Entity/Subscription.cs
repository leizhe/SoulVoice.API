using System;
using DapperExtensions.Mapper;
using SV.Entity.Auditing;

namespace SV.Entity
{
	public sealed class Subscription : BaseEntity, IEntity
	{
		public long AlbumId { get; set; }
		public long? Subscriber { get; set; }
		public DateTime SubscriptionDate { get; set; }
		public Album Album { get; set; }
		[Serializable]
		public sealed class SubscriptionOrmMapper : ClassMapper<Subscription>
		{
			public SubscriptionOrmMapper()
			{
				Table("Subscription");
				Map(f => f.Album).Ignore();
				AutoMap();
			}
		}
	}
}
