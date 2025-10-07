using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddingPropertiesToComponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZIndex",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ZIndex",
                table: "Components");
        }
    }
}
