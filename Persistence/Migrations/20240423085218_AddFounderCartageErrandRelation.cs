﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFounderCartageErrandRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartageErrands_Users_FounderId",
                table: "CartageErrands");

            migrationBuilder.AddForeignKey(
                name: "FK_CartageErrands_Users_FounderId",
                table: "CartageErrands",
                column: "FounderId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartageErrands_Users_FounderId",
                table: "CartageErrands");

            migrationBuilder.AddForeignKey(
                name: "FK_CartageErrands_Users_FounderId",
                table: "CartageErrands",
                column: "FounderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
