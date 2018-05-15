﻿using Microsoft.EntityFrameworkCore;
using SV.Common.Options;
using SV.Entity.Command;
using System;
using System.Configuration;

namespace SV.Repository.Base
{

    
    public sealed class EntityFrameworkContext : DbContext
    {
        public EntityFrameworkContext()
        {
            if (string.IsNullOrEmpty(DbContextOption.CommandString))
                throw new ArgumentNullException(nameof(DbContextOption.CommandString));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(DbContextOption.CommandString);
            base.OnConfiguring(optionsBuilder);
        }

        //private readonly DbContextOption _option;
        //public EntityFrameworkContext(DbContextOptions options) : base(options) { }//Code first
        //public EntityFrameworkContext(DbContextOption option)
        //{
        //    if (option == null)
        //        throw new ArgumentNullException(nameof(option));
        //    if (string.IsNullOrEmpty(option.CommandString))
        //        throw new ArgumentNullException(nameof(option.CommandString));
        //    _option = option;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySql(_option.CommandString);
        //    base.OnConfiguring(optionsBuilder);
        //}

        ////protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        ////  => optionsBuilder
        ////      .UseMySQL(_option.CommandString);


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<Permission>().ToTable("Permission");
            modelBuilder.Entity<RolePermission>().ToTable("RolePermission");
        }
    }
}
