namespace Gah.HC.Repository.Sql.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc/>
    public partial class RenamePercentageAvailable : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder = migrationBuilder ?? throw new ArgumentNullException(nameof(migrationBuilder));
            migrationBuilder.RenameColumn(
                name: "PercentageAvailable",
                table: "Hospitals",
                newName: "PercentOfUsage");

            migrationBuilder.RenameColumn(
                name: "PercentageAvailable",
                table: "HospitalCapacity",
                newName: "PercentOfUsage");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder = migrationBuilder ?? throw new ArgumentNullException(nameof(migrationBuilder));

            migrationBuilder.RenameColumn(
                newName: "PercentageAvailable",
                table: "Hospitals",
                name: "PercentOfUsage");

            migrationBuilder.RenameColumn(
                newName: "PercentageAvailable",
                table: "HospitalCapacity",
                name: "PercentOfUsage");
        }
    }
}
