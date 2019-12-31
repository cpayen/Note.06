using Microsoft.EntityFrameworkCore.Migrations;

namespace Note.Infra.Data.SqlServer.Migrations
{
    public partial class UpdatePages_RemoveAccessProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadAccess",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "WriteAccess",
                table: "Pages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReadAccess",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WriteAccess",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
