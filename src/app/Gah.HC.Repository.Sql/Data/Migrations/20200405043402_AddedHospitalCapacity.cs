using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gah.HC.Repository.Sql.Data.Migrations
{
    public partial class AddedHospitalCapacity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Regions",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsCovid",
                table: "Hospitals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "HospitalCapacity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HospitalId = table.Column<Guid>(nullable: false),
                    BedCapacity = table.Column<int>(nullable: false),
                    BedsInUse = table.Column<int>(nullable: false),
                    PercentageAvailable = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalCapacity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HospitalCapacity_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Name",
                table: "Regions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Slug",
                table: "Regions",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_City",
                table: "Hospitals",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_IsCovid",
                table: "Hospitals",
                column: "IsCovid");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_Name",
                table: "Hospitals",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_PostalCode",
                table: "Hospitals",
                column: "PostalCode");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_State",
                table: "Hospitals",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalCapacity_HospitalId",
                table: "HospitalCapacity",
                column: "HospitalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HospitalCapacity");

            migrationBuilder.DropIndex(
                name: "IX_Regions_Name",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Regions_Slug",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Hospitals_City",
                table: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Hospitals_IsCovid",
                table: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Hospitals_Name",
                table: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Hospitals_PostalCode",
                table: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Hospitals_State",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "IsCovid",
                table: "Hospitals");
        }
    }
}
