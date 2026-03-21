using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookkeeping.Migrations
{
    /// <inheritdoc />
    public partial class CashReceiptOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashReceiptOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DocumentNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SequenceNumber = table.Column<int>(type: "integer", nullable: false),
                    DocumentYear = table.Column<int>(type: "integer", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DebitIfrsAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreditIfrsAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    IncomeCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReferenceBookId = table.Column<Guid>(type: "uuid", nullable: false),
                    VatTaxId = table.Column<Guid>(type: "uuid", nullable: true),
                    Accountant = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Cashier = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashReceiptOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashReceiptOrders_IfrsAccounts_CreditIfrsAccountId",
                        column: x => x.CreditIfrsAccountId,
                        principalTable: "IfrsAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashReceiptOrders_IfrsAccounts_DebitIfrsAccountId",
                        column: x => x.DebitIfrsAccountId,
                        principalTable: "IfrsAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashReceiptOrders_IncomeCategories_IncomeCategoryId",
                        column: x => x.IncomeCategoryId,
                        principalTable: "IncomeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashReceiptOrders_ReferenceBooks_ReferenceBookId",
                        column: x => x.ReferenceBookId,
                        principalTable: "ReferenceBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashReceiptOrders_VatTaxes_VatTaxId",
                        column: x => x.VatTaxId,
                        principalTable: "VatTaxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashReceiptOrders_CreditIfrsAccountId",
                table: "CashReceiptOrders",
                column: "CreditIfrsAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CashReceiptOrders_DebitIfrsAccountId",
                table: "CashReceiptOrders",
                column: "DebitIfrsAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CashReceiptOrders_DocumentYear_SequenceNumber",
                table: "CashReceiptOrders",
                columns: new[] { "DocumentYear", "SequenceNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashReceiptOrders_IncomeCategoryId",
                table: "CashReceiptOrders",
                column: "IncomeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CashReceiptOrders_ReferenceBookId",
                table: "CashReceiptOrders",
                column: "ReferenceBookId");

            migrationBuilder.CreateIndex(
                name: "IX_CashReceiptOrders_VatTaxId",
                table: "CashReceiptOrders",
                column: "VatTaxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashReceiptOrders");
        }
    }
}
