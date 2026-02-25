using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMySettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                schema: "users",
                table: "Users",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "en");

            migrationBuilder.AddColumn<bool>(
                name: "NotificationsEnabled",
                schema: "users",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "PrivacyLevel",
                schema: "users",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "Theme",
                schema: "users",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                schema: "users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NotificationsEnabled",
                schema: "users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PrivacyLevel",
                schema: "users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Theme",
                schema: "users",
                table: "Users");
        }
    }
}
