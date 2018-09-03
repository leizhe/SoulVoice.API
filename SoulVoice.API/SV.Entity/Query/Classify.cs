using System;
using System.Collections.Generic;
using DapperExtensions.Mapper;
using SV.Entity.Auditing;

namespace SV.Entity.Query
{
	public sealed class Classify : BaseEntity, IEntity
	{
		public string Name { get; set; }
		public string Memo { get; set; }
		public ICollection<Album> Albums { get; set; }
		[Serializable]
		public sealed class ClassifyOrmMapper : ClassMapper<Classify>
		{
			public ClassifyOrmMapper()
			{
				Table("Classify");
				Map(f => f.Albums).Ignore();
				Map(f => f.Id).Key(KeyType.Identity);
				AutoMap();
			}
		}
	}
}
