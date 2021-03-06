﻿using System;
using System.Collections.Generic;
using DapperExtensions.Mapper;
using SV.Entity.Auditing;

namespace SV.Entity
{
    public sealed class Menu : BaseEntity, IEntity
    {
        public string No { get; set; }
        public string ParentNo { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public bool IsVisible { get; set; }
        public bool IsLeaf { get; set; }
        public string Pic { get; set; }
		public bool IsDefault { get; set; }
	    public int Client { get; set; }

		public ICollection<Action> Actions { get; set; }
		public Menu()
		{
			Actions = new HashSet<Action>();
		}

		[Serializable]
        public sealed class MenuOrmMapper : ClassMapper<Menu>
        {
            public MenuOrmMapper()
            {
                Table("Menu");
                AutoMap();
            }
        }

    }
}
