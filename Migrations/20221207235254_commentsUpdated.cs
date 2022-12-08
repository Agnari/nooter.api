using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nooter.API.Migrations
{
    public partial class commentsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommenterId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommenterId",
                table: "Comments",
                column: "CommenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_CommenterId",
                table: "Comments",
                column: "CommenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_CommenterId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommenterId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommenterId",
                table: "Comments");
        }
    }
}
