using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SV.Application.Input
{
    public class SubscriptionInput
    {
	    [Required]
		public long AlbumId { get; set; }
	    [Required]
		public long Subscriber { get; set; }
	}
}
