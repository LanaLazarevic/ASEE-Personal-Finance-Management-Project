using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCode",
                schema: "PFM",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Splits_Transactions_TransactionId",
                schema: "PFM",
                table: "Splits");

            migrationBuilder.RenameColumn(
                name: "Mcc",
                schema: "PFM",
                table: "Transactions",
                newName: "mcc");

            migrationBuilder.RenameColumn(
                name: "Kind",
                schema: "PFM",
                table: "Transactions",
                newName: "kind");

            migrationBuilder.RenameColumn(
                name: "Direction",
                schema: "PFM",
                table: "Transactions",
                newName: "direction");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "PFM",
                table: "Transactions",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Currency",
                schema: "PFM",
                table: "Transactions",
                newName: "currency");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "PFM",
                table: "Transactions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "PFM",
                table: "Splits",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                schema: "PFM",
                table: "Splits",
                newName: "transaction_id");

            migrationBuilder.RenameIndex(
                name: "IX_Splits_TransactionId",
                schema: "PFM",
                table: "Splits",
                newName: "IX_Splits_transaction_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "PFM",
                table: "Categories",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "PFM",
                table: "Categories",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "ParentCode",
                schema: "PFM",
                table: "Categories",
                newName: "parent_code");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCode",
                schema: "PFM",
                table: "Categories",
                newName: "IX_Categories_parent_code");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "PFM",
                table: "Transactions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_parent_code",
                schema: "PFM",
                table: "Categories",
                column: "parent_code",
                principalSchema: "PFM",
                principalTable: "Categories",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Splits_Transactions_transaction_id",
                schema: "PFM",
                table: "Splits",
                column: "transaction_id",
                principalSchema: "PFM",
                principalTable: "Transactions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_parent_code",
                schema: "PFM",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Splits_Transactions_transaction_id",
                schema: "PFM",
                table: "Splits");

            migrationBuilder.RenameColumn(
                name: "mcc",
                schema: "PFM",
                table: "Transactions",
                newName: "Mcc");

            migrationBuilder.RenameColumn(
                name: "kind",
                schema: "PFM",
                table: "Transactions",
                newName: "Kind");

            migrationBuilder.RenameColumn(
                name: "direction",
                schema: "PFM",
                table: "Transactions",
                newName: "Direction");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "PFM",
                table: "Transactions",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "currency",
                schema: "PFM",
                table: "Transactions",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "PFM",
                table: "Transactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "PFM",
                table: "Splits",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "transaction_id",
                schema: "PFM",
                table: "Splits",
                newName: "TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Splits_transaction_id",
                schema: "PFM",
                table: "Splits",
                newName: "IX_Splits_TransactionId");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "PFM",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "code",
                schema: "PFM",
                table: "Categories",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "parent_code",
                schema: "PFM",
                table: "Categories",
                newName: "ParentCode");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_parent_code",
                schema: "PFM",
                table: "Categories",
                newName: "IX_Categories_ParentCode");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                schema: "PFM",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCode",
                schema: "PFM",
                table: "Categories",
                column: "ParentCode",
                principalSchema: "PFM",
                principalTable: "Categories",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Splits_Transactions_TransactionId",
                schema: "PFM",
                table: "Splits",
                column: "TransactionId",
                principalSchema: "PFM",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
