using System;
using System.Collections.Generic;
using DapperExtensions.Mapper;

namespace SV.Entity.Query
{
    public sealed class Menu : BaseEntity
    {
        public string No { get; set; }
        public string ParentNo { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public bool IsVisible { get; set; }
        public bool IsLeaf { get; set; }
        public string Pic { get; set; }

        public ICollection<Command.Action> Actions { get; set; }

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
