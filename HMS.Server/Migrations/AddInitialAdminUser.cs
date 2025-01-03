using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using Projet.Services;
using System;

namespace Projet.Migrations
{
    public partial class AddInitialAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var adminId = Guid.NewGuid().ToString();
            var email = "admin@test.com";
            var password = PasswordHasher.HashPassword("Admin123!");
            var role = "Admin";

            migrationBuilder.Sql($@"
                INSERT INTO Users (Id, Email, Password, Role, CreatedAt)
                VALUES ('{adminId}', '{email}', '{password}', '{role}', GETDATE())
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users WHERE Email = 'admin@test.com'");
        }
    }
}