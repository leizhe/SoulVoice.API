﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SV.Repository.Base;
using System;

namespace SV.Repository.Migrations
{
    [DbContext(typeof(EntityFrameworkContext))]
    partial class EntityFrameworkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("SV.Entity.Command.Action", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Icon");

                    b.Property<int>("InitStatus");

                    b.Property<long?>("MenuId");

                    b.Property<string>("Name");

                    b.Property<string>("No");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("Action");
                });

            modelBuilder.Entity("SV.Entity.Command.Album", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("BuyCount");

                    b.Property<long>("ClassifyId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Memo");

                    b.Property<string>("Name");

                    b.Property<string>("Pic");

                    b.Property<long>("PlayCount");

                    b.Property<double>("Price");

                    b.Property<int>("Status");

                    b.Property<long>("SubCount");

                    b.HasKey("Id");

                    b.HasIndex("ClassifyId");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("SV.Entity.Command.AlbumComment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AlbumId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("ParentCommentId");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("AlbumComment");
                });

            modelBuilder.Entity("SV.Entity.Command.Classify", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Memo");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Classify");
                });

            modelBuilder.Entity("SV.Entity.Command.Menu", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Client");

                    b.Property<bool>("IsDefault");

                    b.Property<bool>("IsLeaf");

                    b.Property<bool>("IsVisible");

                    b.Property<string>("Name");

                    b.Property<string>("No");

                    b.Property<int>("Order");

                    b.Property<string>("ParentNo");

                    b.Property<string>("Pic");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("SV.Entity.Command.Permission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Access");

                    b.Property<long>("AccessValue");

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("SV.Entity.Command.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Memo");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("SV.Entity.Command.RolePermission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("PermissionId");

                    b.Property<long>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("SV.Entity.Command.Sound", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AlbumId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Name");

                    b.Property<long>("PlayCount");

                    b.Property<double>("Price");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Sound");
                });

            modelBuilder.Entity("SV.Entity.Command.SoundComment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("ParentCommentId");

                    b.Property<long>("SoundId");

                    b.HasKey("Id");

                    b.HasIndex("SoundId");

                    b.ToTable("SoundComment");
                });

            modelBuilder.Entity("SV.Entity.Command.Subscription", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("AlbumId");

                    b.Property<long?>("Subscriber");

                    b.Property<DateTime>("SubscriptionDate");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Subscription");
                });

            modelBuilder.Entity("SV.Entity.Command.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alipay");

                    b.Property<string>("ApplePay");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<string>("IdCard")
                        .HasMaxLength(50);

                    b.Property<decimal>("Money");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .HasMaxLength(100);

                    b.Property<int>("State");

                    b.Property<string>("WeChat");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("SV.Entity.Command.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("RoleId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("SV.Entity.Command.Action", b =>
                {
                    b.HasOne("SV.Entity.Command.Menu", "Menu")
                        .WithMany("Actions")
                        .HasForeignKey("MenuId");
                });

            modelBuilder.Entity("SV.Entity.Command.Album", b =>
                {
                    b.HasOne("SV.Entity.Command.Classify", "Classify")
                        .WithMany("Albums")
                        .HasForeignKey("ClassifyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SV.Entity.Command.AlbumComment", b =>
                {
                    b.HasOne("SV.Entity.Command.Album", "Album")
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SV.Entity.Command.RolePermission", b =>
                {
                    b.HasOne("SV.Entity.Command.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SV.Entity.Command.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SV.Entity.Command.Sound", b =>
                {
                    b.HasOne("SV.Entity.Command.Album", "Album")
                        .WithMany("Sounds")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SV.Entity.Command.SoundComment", b =>
                {
                    b.HasOne("SV.Entity.Command.Sound", "Sound")
                        .WithMany()
                        .HasForeignKey("SoundId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SV.Entity.Command.Subscription", b =>
                {
                    b.HasOne("SV.Entity.Command.Album", "Album")
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SV.Entity.Command.UserRole", b =>
                {
                    b.HasOne("SV.Entity.Command.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SV.Entity.Command.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
