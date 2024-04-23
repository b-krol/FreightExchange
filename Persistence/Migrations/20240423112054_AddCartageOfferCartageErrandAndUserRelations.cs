using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCartageOfferCartageErrandAndUserRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartageOffers_Users_BidderId",
                table: "CartageOffers");

            migrationBuilder.AddColumn<int>(
                name: "ErrandId",
                table: "CartageOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartageOffers_ErrandId",
                table: "CartageOffers",
                column: "ErrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartageOffers_CartageErrands_ErrandId",
                table: "CartageOffers",
                column: "ErrandId",
                principalTable: "CartageErrands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartageOffers_Users_BidderId",
                table: "CartageOffers",
                column: "BidderId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartageOffers_CartageErrands_ErrandId",
                table: "CartageOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_CartageOffers_Users_BidderId",
                table: "CartageOffers");

            migrationBuilder.DropIndex(
                name: "IX_CartageOffers_ErrandId",
                table: "CartageOffers");

            migrationBuilder.DropColumn(
                name: "ErrandId",
                table: "CartageOffers");

            migrationBuilder.AddForeignKey(
                name: "FK_CartageOffers_Users_BidderId",
                table: "CartageOffers",
                column: "BidderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
