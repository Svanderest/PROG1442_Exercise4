using Microsoft.EntityFrameworkCore.Migrations;

namespace PROG1442_Exercise4.Data.ArtMigrations
{
    public partial class compUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Artworks_Name_ArtTypeID",
                schema: "art",
                table: "Artworks",
                columns: new[] { "Name", "ArtTypeID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Artworks_Name_ArtTypeID",
                schema: "art",
                table: "Artworks");
        }
    }
}
