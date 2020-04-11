namespace Gah.HC.Repository.Sql.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc/>
    public partial class AddedApprovalFlagToUsers : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder = migrationBuilder ?? throw new ArgumentNullException(nameof(migrationBuilder));
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder = migrationBuilder ?? throw new ArgumentNullException(nameof(migrationBuilder));
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "AspNetUsers");
        }
    }
}
