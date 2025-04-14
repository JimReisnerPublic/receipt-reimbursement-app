using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceiptReimbursement.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameImageUrlToImageLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Receipts",
                newName: "ImageLocation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageLocation",
                table: "Receipts",
                newName: "ImageUrl");
        }
    }
}
