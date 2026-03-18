using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialGraph.Infrastructure.Database
{
    /// <inheritdoc />
    public partial class ChangeSchemaSocialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Social");

            migrationBuilder.RenameTable(
                name: "FollowRelationships",
                newName: "FollowRelationships",
                newSchema: "Social");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "FollowRelationships",
                schema: "Social",
                newName: "FollowRelationships");
        }
    }
}
