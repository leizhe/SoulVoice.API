﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Entity.Command
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

        public ICollection<Action> Actions { get; set; }
    }
}