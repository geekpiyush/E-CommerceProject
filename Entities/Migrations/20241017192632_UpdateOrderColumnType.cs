using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class UpdateOrderColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop the primary key constraint (PK_Orders)
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            // Step 2: Drop the existing OrderID column
            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Orders");

            // Step 3: Add the new OrderID column as a string
            migrationBuilder.AddColumn<string>(
                name: "OrderID",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false);

            // Step 4: Recreate the primary key constraint on the new OrderID
            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderID");

            // Modify the TotalPrice column
            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Orders",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop the primary key on the new OrderID
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            // Step 2: Drop the new OrderID column (string)
            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "Orders");

            // Step 3: Recreate the old OrderID column with int and identity
            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "Orders",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Step 4: Recreate the primary key on the old OrderID
            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderID");

            // Revert the TotalPrice column
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
