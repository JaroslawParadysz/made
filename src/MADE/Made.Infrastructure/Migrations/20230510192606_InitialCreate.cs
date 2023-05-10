using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Made.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "made");

            migrationBuilder.CreateTable(
                name: "Batches",
                schema: "made",
                columns: table => new
                {
                    Reference = table.Column<Guid>(type: "uuid", nullable: false),
                    SKU = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Eta = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batches", x => x.Reference);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "made",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                schema: "made",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SKU = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    BatchReference = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLines_Batches_BatchReference",
                        column: x => x.BatchReference,
                        principalSchema: "made",
                        principalTable: "Batches",
                        principalColumn: "Reference");
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "made",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_BatchReference",
                schema: "made",
                table: "OrderLines",
                column: "BatchReference");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                schema: "made",
                table: "OrderLines",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLines",
                schema: "made");

            migrationBuilder.DropTable(
                name: "Batches",
                schema: "made");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "made");
        }
    }
}
