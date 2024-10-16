using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnProductDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Quantity",
                table: "ProductData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ProductDescription",
                table: "ProductData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ProductData",
                keyColumn: "ProductID",
                keyValue: 11001,
                column: "ProductDescription",
                value: "For a gentleman who wears his heart on his sleeve and knows self-love is the greatest virtue");

            migrationBuilder.UpdateData(
                table: "ProductData",
                keyColumn: "ProductID",
                keyValue: 11002,
                column: "ProductDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductData",
                keyColumn: "ProductID",
                keyValue: 11003,
                column: "ProductDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductData",
                keyColumn: "ProductID",
                keyValue: 11004,
                column: "ProductDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductData",
                keyColumn: "ProductID",
                keyValue: 11005,
                column: "ProductDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductData",
                keyColumn: "ProductID",
                keyValue: 11006,
                column: "ProductDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductData",
                keyColumn: "ProductID",
                keyValue: 11007,
                column: "ProductDescription",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProductData",
                keyColumn: "ProductID",
                keyValue: 11008,
                column: "ProductDescription",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDescription",
                table: "ProductData");

            migrationBuilder.AlterColumn<string>(
                name: "Quantity",
                table: "ProductData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
