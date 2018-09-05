using System;
using System.Collections.Generic;
using System.Text;
using SV.Application.Dtos;

namespace SV.Application.Input
{
    public class AlbumInput
	{
		public long? Id { get; set; }
		public long ClassifyId { get; set; }
	    public string Name { get; set; }
	    public string Memo { get; set; }
	    public string Pic { get; set; }
	    public double? Price { get; set; }
	    public long? CreatorUserId { get; set; }
	}
}
