using Microsoft.EntityFrameworkCore.Migrations;

namespace Note.Infra.Data.SqlServer.Migrations
{
    public partial class UpdateBookmark_RemoveTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_Title",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Bookmarks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Bookmarks",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_Title",
                table: "Bookmarks",
                column: "Title");
        }
    }
}
