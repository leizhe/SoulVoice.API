using System;
using SV.Entity.Auditing;

namespace SV.Entity.Command
{
	public sealed class AlbumComment : BaseEntity, ICreationAudited
	{
		public long AlbumId { get; set; }
		public long? ParentCommentId { get; set; }
		public string Content { get; set; }
		public long? CreatorUserId { get; set; }
		public DateTime CreationTime { get; set; }
		public Album Album { get; set; }
	}
}
