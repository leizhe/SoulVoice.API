using System;
using DapperExtensions.Mapper;

namespace SV.Entity.Query
{
	public sealed class Subscription : BaseEntity
	{
		public long AlbumId { get; set; }
		public long? Subscriber { get; set; }
		public DateTime SubscriptionDate { get; set; }
		[Serializable]
		public sealed class SubscriptionOrmMapper : ClassMapper<Subscription>
		{
			public SubscriptionOrmMapper()
			{
				Table("Subscription");
				AutoMap();
			}
		}
	}
}
