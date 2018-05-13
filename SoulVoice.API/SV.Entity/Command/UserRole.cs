namespace SV.Entity.Command
{
    public sealed  class UserRole : BaseEntity
    {
        public long RoleId { get; set; }
        public long UserId { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}
