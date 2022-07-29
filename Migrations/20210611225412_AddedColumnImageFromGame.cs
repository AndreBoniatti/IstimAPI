using Microsoft.EntityFrameworkCore.Migrations;

namespace IstimAPI.Migrations
{
    public partial class AddedColumnImageFromGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Games");
        }
    }
}
