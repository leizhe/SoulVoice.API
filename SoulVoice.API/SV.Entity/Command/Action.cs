namespace SV.Entity.Command
{
    public sealed class Action : BaseEntity
    {
        public string Name { get; set; }
        public string No { get; set; }
        public int InitStatus { get; set; }
        public string Icon { get; set; }
        public Menu Menu { get; set; }
    }
}
