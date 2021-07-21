using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Migrations
{
    public partial class AddData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameProduct",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                nullable: true);
            migrationBuilder.InsertData("Products", new string[] { "Price", "Manufacture","Name","Type"}, new string[] { "200","Lenovo","DTO-2000", "1" }, "dbo");
            }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "NameProduct",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
