using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Migrations
{
    public partial class AddProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Products", new string[] { "Price", "Manufacture", "Name", "Type" }, new string[] { "452", "Samsung", "S8", "1" }, "dbo");
            migrationBuilder.InsertData("Products", new string[] { "Price", "Manufacture", "Name", "Type" }, new string[] { "112", "Sony", "GTX-2080", "3" }, "dbo");
            migrationBuilder.InsertData("Products", new string[] { "Price", "Manufacture", "Name", "Type" }, new string[] { "3423", "Samsung", "GTX-3080", "2" }, "dbo");
            migrationBuilder.InsertData("Products", new string[] { "Price", "Manufacture", "Name", "Type" }, new string[] { "324", "Sony", "PL-5", "3" }, "dbo");
            migrationBuilder.InsertData("Products", new string[] { "Price", "Manufacture", "Name", "Type" }, new string[] { "111", "Samsung", "Tab S0", "2" }, "dbo");
            migrationBuilder.InsertData("Products", new string[] { "Price", "Manufacture", "Name", "Type" }, new string[] { "22", "Lenovo", "DDL-122", "1" }, "dbo");
            migrationBuilder.InsertData("Products", new string[] { "Price", "Manufacture", "Name", "Type" }, new string[] { "20", "Samsung", "SmartChair", "2" }, "dbo");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
