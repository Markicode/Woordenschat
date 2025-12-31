using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UseDateOnlyForPublishedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "Books",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedDate",
                table: "Books",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
