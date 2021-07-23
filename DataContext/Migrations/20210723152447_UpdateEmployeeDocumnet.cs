using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class UpdateEmployeeDocumnet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HomePhoneNumber",
                table: "employeeDocument",
                newName: "PersonalCode");

            migrationBuilder.RenameColumn(
                name: "BankAccountNumber",
                table: "employeeDocument",
                newName: "CertificateCode");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDay",
                table: "employeeDocument",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BirthdayLocation",
                table: "employeeDocument",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyName",
                table: "employeeDocument",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "employeeDocument",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldOfStudy",
                table: "employeeDocument",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HealthStatus",
                table: "employeeDocument",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthStatusDescription",
                table: "employeeDocument",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HistoryOfDetention",
                table: "employeeDocument",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistoryOfDetentionDescription",
                table: "employeeDocument",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobRank",
                table: "employeeDocument",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastEducationalCertificate",
                table: "employeeDocument",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastWorkExperience",
                table: "employeeDocument",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MaritalStatus",
                table: "employeeDocument",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MilitaryService",
                table: "employeeDocument",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MilitaryServiceDesCription",
                table: "employeeDocument",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Obligation",
                table: "employeeDocument",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PersonalCodeDate",
                table: "employeeDocument",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalCodeLocation",
                table: "employeeDocument",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Religion",
                table: "employeeDocument",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "BirthdayLocation",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "FamilyName",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "FieldOfStudy",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "HealthStatus",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "HealthStatusDescription",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "HistoryOfDetention",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "HistoryOfDetentionDescription",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "JobRank",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "LastEducationalCertificate",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "LastWorkExperience",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "MilitaryService",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "MilitaryServiceDesCription",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "Obligation",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "PersonalCodeDate",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "PersonalCodeLocation",
                table: "employeeDocument");

            migrationBuilder.DropColumn(
                name: "Religion",
                table: "employeeDocument");

            migrationBuilder.RenameColumn(
                name: "PersonalCode",
                table: "employeeDocument",
                newName: "HomePhoneNumber");

            migrationBuilder.RenameColumn(
                name: "CertificateCode",
                table: "employeeDocument",
                newName: "BankAccountNumber");
        }
    }
}
