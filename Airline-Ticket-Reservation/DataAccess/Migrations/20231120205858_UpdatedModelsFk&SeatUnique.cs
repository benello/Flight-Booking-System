using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class UpdatedModelsFkSeatUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Passport_PassportFk",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Flight_FlightFk",
                table: "Seat");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Flight_FlightFk",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Passport_PassportFk",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Seat_SeatFk",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_PassportFk",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_SeatFk",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "PassportFk",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "SeatFk",
                table: "Ticket",
                newName: "SeatId");

            migrationBuilder.RenameColumn(
                name: "FlightFk",
                table: "Ticket",
                newName: "FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_FlightFk",
                table: "Ticket",
                newName: "IX_Ticket_FlightId");

            migrationBuilder.RenameColumn(
                name: "FlightFk",
                table: "Seat",
                newName: "FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_FlightFk_RowNumber_ColumnNumber",
                table: "Seat",
                newName: "IX_Seat_FlightId_RowNumber_ColumnNumber");

            migrationBuilder.RenameColumn(
                name: "PassportFk",
                table: "AspNetUsers",
                newName: "PassportNumber");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_PassportFk",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_PassportNumber");

            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "Ticket",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_PassportNumber",
                table: "Ticket",
                column: "PassportNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SeatId",
                table: "Ticket",
                column: "SeatId",
                unique: true,
                filter: "[SeatId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Passport_PassportNumber",
                table: "AspNetUsers",
                column: "PassportNumber",
                principalTable: "Passport",
                principalColumn: "PassportNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Flight_FlightId",
                table: "Seat",
                column: "FlightId",
                principalTable: "Flight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Flight_FlightId",
                table: "Ticket",
                column: "FlightId",
                principalTable: "Flight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Passport_PassportNumber",
                table: "Ticket",
                column: "PassportNumber",
                principalTable: "Passport",
                principalColumn: "PassportNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Seat_SeatId",
                table: "Ticket",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Passport_PassportNumber",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Flight_FlightId",
                table: "Seat");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Flight_FlightId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Passport_PassportNumber",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Seat_SeatId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_PassportNumber",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_SeatId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "SeatId",
                table: "Ticket",
                newName: "SeatFk");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "Ticket",
                newName: "FlightFk");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_FlightId",
                table: "Ticket",
                newName: "IX_Ticket_FlightFk");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "Seat",
                newName: "FlightFk");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_FlightId_RowNumber_ColumnNumber",
                table: "Seat",
                newName: "IX_Seat_FlightFk_RowNumber_ColumnNumber");

            migrationBuilder.RenameColumn(
                name: "PassportNumber",
                table: "AspNetUsers",
                newName: "PassportFk");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_PassportNumber",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_PassportFk");

            migrationBuilder.AddColumn<string>(
                name: "PassportFk",
                table: "Ticket",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_PassportFk",
                table: "Ticket",
                column: "PassportFk");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SeatFk",
                table: "Ticket",
                column: "SeatFk",
                unique: true,
                filter: "[SeatFk] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Passport_PassportFk",
                table: "AspNetUsers",
                column: "PassportFk",
                principalTable: "Passport",
                principalColumn: "PassportNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Flight_FlightFk",
                table: "Seat",
                column: "FlightFk",
                principalTable: "Flight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Flight_FlightFk",
                table: "Ticket",
                column: "FlightFk",
                principalTable: "Flight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Passport_PassportFk",
                table: "Ticket",
                column: "PassportFk",
                principalTable: "Passport",
                principalColumn: "PassportNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Seat_SeatFk",
                table: "Ticket",
                column: "SeatFk",
                principalTable: "Seat",
                principalColumn: "Id");
        }
    }
}
