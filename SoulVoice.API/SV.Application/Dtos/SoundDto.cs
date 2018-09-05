using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Application.Dtos
{
    public class SoundDto
    {
	    public long AlbumId { get; set; }
	    public string Name { get; set; }
	    public string Url { get; set; }
	    public long PlayCount { get; set; }
	    public double Price { get; set; }
	    public long? CreatorUserId { get; set; }
	    public DateTime CreationTime { get; set; }
	}
}
