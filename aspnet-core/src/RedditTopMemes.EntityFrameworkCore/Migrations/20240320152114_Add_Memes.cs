using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedditTopMemes.Migrations
{
    /// <inheritdoc />
    public partial class Add_Memes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MemeId",
                table: "TopMemeItems",
                type: "nvarchar(7)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Memes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopMemeItems_MemeId",
                table: "TopMemeItems",
                column: "MemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopMemeItems_Memes_MemeId",
                table: "TopMemeItems",
                column: "MemeId",
                principalTable: "Memes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopMemeItems_Memes_MemeId",
                table: "TopMemeItems");

            migrationBuilder.DropTable(
                name: "Memes");

            migrationBuilder.DropIndex(
                name: "IX_TopMemeItems_MemeId",
                table: "TopMemeItems");

            migrationBuilder.DropColumn(
                name: "MemeId",
                table: "TopMemeItems");
        }
    }
}
