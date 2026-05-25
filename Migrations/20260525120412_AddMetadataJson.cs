using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authservice.Migrations
{
    /// <inheritdoc />
    public partial class AddMetadataJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetadataJson",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetadataJson",
                table: "Answer",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetadataJson",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "MetadataJson",
                table: "Answer");
        }
    }
}
