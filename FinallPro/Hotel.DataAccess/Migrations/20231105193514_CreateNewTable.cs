using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.DataAccess.Migrations
{
    public partial class CreateNewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "RoomDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_RoomDetails_RoomDetailId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_RoomDetailId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "RoomDetails");

            migrationBuilder.DropColumn(
                name: "RoomDetailId",
                table: "Images");
        }
    }
}
