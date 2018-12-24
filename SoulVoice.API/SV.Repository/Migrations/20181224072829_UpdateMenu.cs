using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SV.Repository.Migrations
{
    public partial class UpdateMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Client",
                table: "Menu",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Menu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_AlbumId",
                table: "Subscription",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Album_AlbumId",
                table: "Subscription",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Album_AlbumId",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_AlbumId",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "Client",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Menu");
        }
    }
}
