using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaWeb.Persistence.Migrations
{
    public partial class removePageCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageCount",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "StorageLocation",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "TotalSize",
                table: "Chapters");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedDate",
                table: "Chapters",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Chapters",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageCount",
                table: "Chapters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StorageLocation",
                table: "Chapters",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "TotalSize",
                table: "Chapters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
