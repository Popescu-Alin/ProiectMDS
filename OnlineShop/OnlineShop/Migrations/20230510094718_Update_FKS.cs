using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class Update_FKS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Addresses_UserId",
                table: "UserAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCards_Cards_UserId",
                table: "UserCards");

            migrationBuilder.CreateIndex(
                name: "IX_UserCards_CardId",
                table: "UserCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_AddressId",
                table: "UserAddresses",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Addresses_AddressId",
                table: "UserAddresses",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCards_Cards_CardId",
                table: "UserCards",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Addresses_AddressId",
                table: "UserAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCards_Cards_CardId",
                table: "UserCards");

            migrationBuilder.DropIndex(
                name: "IX_UserCards_CardId",
                table: "UserCards");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_AddressId",
                table: "UserAddresses");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Addresses_UserId",
                table: "UserAddresses",
                column: "UserId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCards_Cards_UserId",
                table: "UserCards",
                column: "UserId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
