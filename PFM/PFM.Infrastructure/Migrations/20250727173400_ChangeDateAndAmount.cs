using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateAndAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                schema: "PFM",
                table: "Transactions",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "PFM",
                table: "Transactions",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "PFM",
                table: "Splits",
                newName: "amount");

            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                schema: "PFM",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "date",
                schema: "PFM",
                table: "Transactions",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "amount",
                schema: "PFM",
                table: "Transactions",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "amount",
                schema: "PFM",
                table: "Splits",
                newName: "Amount");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "PFM",
                table: "Transactions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
