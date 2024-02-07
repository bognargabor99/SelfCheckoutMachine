using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SelfCheckoutMachine.Migrations
{
    /// <inheritdoc />
    public partial class DenominationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Denominations",
                table: "Denominations");

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Value",
                keyValue: "10");

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Value",
                keyValue: "100");

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Value",
                keyValue: "1000");

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Value",
                keyValue: "10000");

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Value",
                keyValue: "20");

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Value",
                keyValue: "200");

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Value",
                keyValue: "2000");

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Value",
                keyValue: "20000");

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Value",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Value",
                keyValue: "50");

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Value",
                keyValue: "500");

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Value",
                keyValue: "5000");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Denominations",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Denominations",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Denominations",
                table: "Denominations",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Denominations",
                columns: new[] { "Id", "Amount", "Value" },
                values: new object[,]
                {
                    { 1, 0u, "5" },
                    { 2, 0u, "10" },
                    { 3, 0u, "20" },
                    { 4, 0u, "50" },
                    { 5, 0u, "100" },
                    { 6, 0u, "200" },
                    { 7, 0u, "500" },
                    { 8, 0u, "1000" },
                    { 9, 0u, "2000" },
                    { 10, 0u, "5000" },
                    { 11, 0u, "10000" },
                    { 12, 0u, "20000" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Denominations",
                table: "Denominations");

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Denominations",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 12);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Denominations");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Denominations",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Denominations",
                table: "Denominations",
                column: "Value");

            migrationBuilder.InsertData(
                table: "Denominations",
                columns: new[] { "Value", "Amount" },
                values: new object[,]
                {
                    { "10", 0u },
                    { "100", 0u },
                    { "1000", 0u },
                    { "10000", 0u },
                    { "20", 0u },
                    { "200", 0u },
                    { "2000", 0u },
                    { "20000", 0u },
                    { "5", 0u },
                    { "50", 0u },
                    { "500", 0u },
                    { "5000", 0u }
                });
        }
    }
}
