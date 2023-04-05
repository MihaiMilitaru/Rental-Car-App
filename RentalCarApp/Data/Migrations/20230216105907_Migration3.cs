using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCarApp.Data.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Transimission",
                table: "Vehicles",
                newName: "Transmission");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Vehicles",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "Transmission",
                table: "Vehicles",
                newName: "Transimission");
        }
    }
}
