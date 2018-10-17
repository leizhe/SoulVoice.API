using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Application.Dtos
{
    public class SubscriptionDto
    {
	    public string AlbumName { get; set; }
	    public string AlbumPic { get; set; }
	    public string AlbumMemo { get; set; }
		public DateTime AlbumLastUpdate { get; set; }
	    public string CreatorUserName { get; set; }
		public long? Subscriber { get; set; }
	    public DateTime SubscriptionDate { get; set; }
	}
}
