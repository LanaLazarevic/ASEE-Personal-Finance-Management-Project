using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PFM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSplitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Splits",
                schema: "PFM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionId = table.Column<string>(type: "text", nullable: false),
                    catcode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Amount = table.Column<double>(type: "numeric(20,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Splits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Splits_Categories_catcode",
                        column: x => x.catcode,
                        principalSchema: "PFM",
                        principalTable: "Categories",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Splits_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalSchema: "PFM",
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Splits_catcode",
                schema: "PFM",
                table: "Splits",
                column: "catcode");

            migrationBuilder.CreateIndex(
                name: "IX_Splits_TransactionId",
                schema: "PFM",
                table: "Splits",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Splits",
                schema: "PFM");
        }
    }
}
