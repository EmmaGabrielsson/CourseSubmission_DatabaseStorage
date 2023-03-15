using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseSubmission_DatabaseStorage.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Clients_Email",
                table: "Clients",
                column: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Clients_Email",
                table: "Clients");
        }
    }
}
