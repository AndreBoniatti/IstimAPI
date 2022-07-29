using Microsoft.EntityFrameworkCore.Migrations;

namespace IstimAPI.Migrations
{
    public partial class AddedColumnValueFromGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Games",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Games");
        }
    }
}
