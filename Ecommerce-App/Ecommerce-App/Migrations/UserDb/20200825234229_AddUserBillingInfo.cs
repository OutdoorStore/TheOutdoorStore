using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_App.Migrations.UserDb
{
    public partial class AddUserBillingInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillingAddress",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingCity",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingState",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingZip",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BillingCity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BillingState",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BillingZip",
                table: "AspNetUsers");
        }
    }
}
