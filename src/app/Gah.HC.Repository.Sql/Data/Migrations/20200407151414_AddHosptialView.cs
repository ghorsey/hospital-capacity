namespace Gah.HC.Repository.Sql.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc/>
    public partial class AddHosptialView : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder = migrationBuilder ?? throw new ArgumentNullException(nameof(migrationBuilder));
            migrationBuilder.RenameIndex(
                name: "AK_SLUG",
                table: "Hospitals",
                newName: "AK_HOSPITAL_SLUG");

            migrationBuilder.RenameIndex(
                name: "IX_Region",
                table: "Hospitals",
                newName: "IX_HOSPITAL_Region");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Hospitals",
                maxLength: 20,
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.CreateTable(
                name: "HospitalView",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Address1 = table.Column<string>(maxLength: 100, nullable: false),
                    Address2 = table.Column<string>(maxLength: 100, nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    State = table.Column<string>(maxLength: 2, nullable: false),
                    PostalCode = table.Column<string>(maxLength: 20, nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    IsCovid = table.Column<bool>(nullable: false),
                    BedCapacity = table.Column<int>(nullable: false),
                    BedsInUse = table.Column<int>(nullable: false),
                    PercentOfUsage = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    Slug = table.Column<string>(maxLength: 402, nullable: false),
                    RegionId = table.Column<Guid>(nullable: false),
                    RegionName = table.Column<string>(maxLength: 50, nullable: false),
                    Capacity1 = table.Column<int>(nullable: false),
                    Used1 = table.Column<int>(nullable: false),
                    Capacity2 = table.Column<int>(nullable: false),
                    Used2 = table.Column<int>(nullable: false),
                    Capacity3 = table.Column<int>(nullable: false),
                    Used3 = table.Column<int>(nullable: false),
                    Capacity4 = table.Column<int>(nullable: false),
                    Used4 = table.Column<int>(nullable: false),
                    Capacity5 = table.Column<int>(nullable: false),
                    Used5 = table.Column<int>(nullable: false),
                    Capacity6 = table.Column<int>(nullable: false),
                    Used6 = table.Column<int>(nullable: false),
                    Capacity7 = table.Column<int>(nullable: false),
                    Used7 = table.Column<int>(nullable: false),
                    Capacity8 = table.Column<int>(nullable: false),
                    Used8 = table.Column<int>(nullable: false),
                    Capacity9 = table.Column<int>(nullable: false),
                    Used9 = table.Column<int>(nullable: false),
                    Capacity10 = table.Column<int>(nullable: false),
                    Used10 = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalView", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HOSPITALCAPACITY_CREATEDON",
                table: "HospitalCapacity",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalView_City",
                table: "HospitalView",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalView_IsCovid",
                table: "HospitalView",
                column: "IsCovid");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalView_Name",
                table: "HospitalView",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalView_PostalCode",
                table: "HospitalView",
                column: "PostalCode");

            migrationBuilder.CreateIndex(
                name: "IX_HOSPITALVIEW_Region",
                table: "HospitalView",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "AK_HOSPITALVIEW_SLUG",
                table: "HospitalView",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HospitalView_State",
                table: "HospitalView",
                column: "State");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder = migrationBuilder ?? throw new ArgumentNullException(nameof(migrationBuilder));
            migrationBuilder.DropTable(
                name: "HospitalView");

            migrationBuilder.DropIndex(
                name: "IX_HOSPITALCAPACITY_CREATEDON",
                table: "HospitalCapacity");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Hospitals");

            migrationBuilder.RenameIndex(
                name: "AK_HOSPITAL_SLUG",
                table: "Hospitals",
                newName: "AK_SLUG");

            migrationBuilder.RenameIndex(
                name: "IX_HOSPITAL_Region",
                table: "Hospitals",
                newName: "IX_Region");
        }
    }
}
