using System;

namespace SV.Entity.Command
{
	public sealed class Subscription : BaseEntity
	{
		public long AlbumId { get; set; }
		public long? Subscriber { get; set; }
		public DateTime SubscriptionDate { get; set; }
	}
}
