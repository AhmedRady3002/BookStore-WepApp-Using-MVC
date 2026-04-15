using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class editeDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Authors_Author_no",
                table: "AuthorBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Books_Bookcode",
                table: "AuthorBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Authors_Author_no",
                table: "AuthorBooks",
                column: "Author_no",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Books_Bookcode",
                table: "AuthorBooks",
                column: "Bookcode",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Authors_Author_no",
                table: "AuthorBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Books_Bookcode",
                table: "AuthorBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Authors_Author_no",
                table: "AuthorBooks",
                column: "Author_no",
                principalTable: "Authors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Books_Bookcode",
                table: "AuthorBooks",
                column: "Bookcode",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
