using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SelfCheckoutMachine.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Denominations",
                columns: table => new
                {
                    Value = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Amount = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Denominations", x => x.Value);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Denominations");
        }
    }
}
