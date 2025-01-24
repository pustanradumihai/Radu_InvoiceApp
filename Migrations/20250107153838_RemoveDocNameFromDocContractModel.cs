using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RadocInvoice.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDocNameFromDocContractModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "DoctorContracts");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorContracts_DoctorId",
                table: "DoctorContracts",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorContracts_Doctors_DoctorId",
                table: "DoctorContracts",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorContracts_Doctors_DoctorId",
                table: "DoctorContracts");

            migrationBuilder.DropIndex(
                name: "IX_DoctorContracts_DoctorId",
                table: "DoctorContracts");

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "DoctorContracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
