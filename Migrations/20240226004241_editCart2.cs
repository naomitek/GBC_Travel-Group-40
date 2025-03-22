using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBC_Travel_Group_40.Migrations
{
    /// <inheritdoc />
    public partial class editCart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomBookingId",
                table: "Carts",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "FlightBookingId",
                table: "Carts",
                newName: "FlightId");

            migrationBuilder.RenameColumn(
                name: "CarBookingId",
                table: "Carts",
                newName: "CarId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Carts",
                newName: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Carts",
                newName: "RoomBookingId");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "Carts",
                newName: "FlightBookingId");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "Carts",
                newName: "CarBookingId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Carts",
                newName: "ItemId");
        }
    }
}
