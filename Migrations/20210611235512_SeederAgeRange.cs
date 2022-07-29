using Microsoft.EntityFrameworkCore.Migrations;

namespace IstimAPI.Migrations
{
    public partial class SeederAgeRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AgeRanges",
                columns: new[] { "Id", "Range", "Title" },
                values: new object[,]
                {
                    { 1, 0, "L" },
                    { 2, 10, "10+" },
                    { 3, 12, "12+" },
                    { 4, 14, "14+" },
                    { 5, 16, "16+" },
                    { 6, 18, "18+" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AgeRanges",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AgeRanges",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AgeRanges",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AgeRanges",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AgeRanges",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AgeRanges",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
