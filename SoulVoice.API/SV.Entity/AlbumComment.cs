using System;
using DapperExtensions.Mapper;
using SV.Entity.Auditing;

namespace SV.Entity
{
	public sealed class AlbumComment : BaseEntity, ICreationAudited
	{
		public long AlbumId { get; set; }
		public long? ParentCommentId { get; set; }
		public string Content { get; set; }
		public long? CreatorUserId { get; set; }
		public DateTime CreationTime { get; set; }
		public Album Album { get; set; }
		[Serializable]
		public sealed class AlbumCommentOrmMapper : ClassMapper<AlbumComment>
		{
			public AlbumCommentOrmMapper()
			{
				Table("AlbumComment");
				AutoMap();
			}
		}
	}
}
