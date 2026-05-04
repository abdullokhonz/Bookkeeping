using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookkeeping.Migrations
{
    /// <inheritdoc />
    public partial class ReferenceBooksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReferenceBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ReferenceBookCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubIfrsAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Info = table.Column<Dictionary<string, object>>(type: "jsonb", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferenceBooks_IfrsAccounts_SubIfrsAccountId",
                        column: x => x.SubIfrsAccountId,
                        principalTable: "IfrsAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReferenceBooks_ReferenceBookCategories_ReferenceBookCategor~",
                        column: x => x.ReferenceBookCategoryId,
                        principalTable: "ReferenceBookCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceBooks_ReferenceBookCategoryId",
                table: "ReferenceBooks",
                column: "ReferenceBookCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceBooks_SubIfrsAccountId",
                table: "ReferenceBooks",
                column: "SubIfrsAccountId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReferenceBooks");
        }
    }
}
