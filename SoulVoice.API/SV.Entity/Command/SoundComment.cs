using System;
using System.Collections.Generic;
using System.Text;
using SV.Entity.Auditing;

namespace SV.Entity.Command
{
	public sealed class SoundComment : BaseEntity, ICreationAudited
	{
		public long SoundId { get; set; }
		public long? ParentCommentId { get; set; }
		public string Content { get; set; }
		public long? CreatorUserId { get; set; }
		public DateTime CreationTime { get; set; }
		public Sound Sound { get; set; }
	}
}
