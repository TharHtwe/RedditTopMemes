using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedditTopMemes.Migrations
{
    /// <inheritdoc />
    public partial class Remove_MemeInfos_from_TopMemeItems_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "TopMemeItems");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "TopMemeItems");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "TopMemeItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "TopMemeItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "TopMemeItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "TopMemeItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
