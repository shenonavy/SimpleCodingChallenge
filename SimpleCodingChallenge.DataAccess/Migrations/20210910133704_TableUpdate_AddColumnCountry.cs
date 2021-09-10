using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleCodingChallenge.DataAccess.Migrations
{
    public partial class TableUpdate_AddColumnCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "N/A");

            migrationBuilder.Sql("update Employees set Country = 'N/A'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Employees");
        }
    }
}
