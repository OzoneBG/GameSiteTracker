using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GST.Data.Migrations
{
    public partial class Owner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Pages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_AuthorId",
                table: "Pages",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_AspNetUsers_AuthorId",
                table: "Pages",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_AspNetUsers_AuthorId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_AuthorId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Pages");
        }
    }
}
