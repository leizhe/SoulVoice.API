using System;
using System.Collections.Generic;
using DapperExtensions.Mapper;

namespace SV.Entity.Query
{
	public sealed class Classify : BaseEntity
	{
		public string Name { get; set; }
		public string Memo { get; set; }
		public ICollection<Command.Album> Albums { get; set; }
		[Serializable]
		public sealed class ClassifyOrmMapper : ClassMapper<Classify>
		{
			public ClassifyOrmMapper()
			{
				Table("Classify");
				AutoMap();
			}
		}
	}
}
