using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.DataAccess.Migrations
{
    public partial class CreateNewbosTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_RoomDetails_RoomDetailId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_RoomDetailId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "RoomDetailId",
                table: "Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomDetailId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_RoomDetailId",
                table: "Images",
                column: "RoomDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_RoomDetails_RoomDetailId",
                table: "Images",
                column: "RoomDetailId",
                principalTable: "RoomDetails",
                principalColumn: "Id");
        }
    }
}
