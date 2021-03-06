﻿using System;
using DapperExtensions.Mapper;

namespace SV.Entity
{
    public sealed class Action : BaseEntity
    {
        public string Name { get; set; }
        public string No { get; set; }
        public int InitStatus { get; set; }
        public string Icon { get; set; }
		public long MenuId { get; set; }
        [Serializable]
        public sealed class ActionOrmMapper : ClassMapper<Action>
        {
            public ActionOrmMapper()
            {
                Table("Action");
                AutoMap();
            }
        }
    }
}
