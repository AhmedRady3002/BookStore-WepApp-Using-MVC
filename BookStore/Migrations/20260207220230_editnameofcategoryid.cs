using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class editnameofcategoryid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoruID",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "CategoruID",
                table: "Books",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CategoruID",
                table: "Books",
                newName: "IX_Books_CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoryID",
                table: "Books",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Categories_CategoryID",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Books",
                newName: "CategoruID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_CategoryID",
                table: "Books",
                newName: "IX_Books_CategoruID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Categories_CategoruID",
                table: "Books",
                column: "CategoruID",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
