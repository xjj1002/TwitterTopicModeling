using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterTopicModeling.Migrations
{
    public partial class RepoprtFlad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "malFlag",
                table: "Report",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "malFlag",
                table: "Report");
        }
    }
}
