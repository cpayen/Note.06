using Microsoft.EntityFrameworkCore.Migrations;

namespace Note.Infra.Data.SqlServer.Migrations
{
    public partial class UpdatePage_AddPageType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Pages",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Pages");
        }
    }
}
