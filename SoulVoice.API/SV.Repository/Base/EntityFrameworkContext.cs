using System;
using Microsoft.EntityFrameworkCore;
using SV.Common.Options;
using SV.Entity.Command;

namespace SV.Repository.Base
{
    public sealed class EntityFrameworkContext : DbContext
    {
        private readonly DbContextOption _option;
        //public EntityFrameworkContext(DbContextOptions options) : base(options) { }//Code first
        public EntityFrameworkContext(DbContextOption option)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));
            if (string.IsNullOrEmpty(option.CommandString))
                throw new ArgumentNullException(nameof(option.CommandString));
            _option = option;
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL(_option.CommandString);
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder
              .UseMySQL(_option.CommandString);


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
        }
    }
}