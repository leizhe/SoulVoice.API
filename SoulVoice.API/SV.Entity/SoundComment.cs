using System;
using DapperExtensions.Mapper;
using SV.Entity.Auditing;

namespace SV.Entity
{
	public sealed class SoundComment : BaseEntity, ICreationAudited
	{
		public long SoundId { get; set; }
		public long? ParentCommentId { get; set; }
		public string Content { get; set; }
		public long? CreatorUserId { get; set; }
		public DateTime CreationTime { get; set; }
		public Sound Sound { get; set; }
		[Serializable]
		public sealed class SoundCommentOrmMapper : ClassMapper<SoundComment>
		{
			public SoundCommentOrmMapper()
			{
				Table("SoundComment");
				AutoMap();
			}
		}
	}
}
