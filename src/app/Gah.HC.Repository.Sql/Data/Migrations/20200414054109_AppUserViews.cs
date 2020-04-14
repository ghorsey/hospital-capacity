namespace Gah.HC.Repository.Sql.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

        /// <inheritdoc/>
    public partial class AppUserViews : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder = migrationBuilder ?? throw new ArgumentNullException(nameof(migrationBuilder));
            migrationBuilder.CreateTable(
                name: "AppUserView",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    UserType = table.Column<int>(nullable: false),
                    RegionId = table.Column<Guid>(nullable: true),
                    RegionName = table.Column<string>(maxLength: 50, nullable: true),
                    HospitalId = table.Column<Guid>(nullable: true),
                    HospitalName = table.Column<string>(maxLength: 200, nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserView", x => x.Id);
                });
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder = migrationBuilder ?? throw new ArgumentNullException(nameof(migrationBuilder));
            migrationBuilder.DropTable(
                name: "AppUserView");
        }
    }
}
