using Projet.Entities;
using Projet.Services;
using Projet.Enums;
using Projet.Context;

public static class DatabaseSeeder
{
    public static void SeedUsers(ApplicationDbContext context)
    {
        if (context.Users.Any()) return;

        var users = new List<User>
        {
            new User 
            { 
                Email = "admin@hospital.com",
                Password = PasswordHasher.HashPassword("Admin123!"),
                Role = Role.Admin
            },
            new User 
            { 
                Email = "doctor@hospital.com",
                Password = PasswordHasher.HashPassword("Doctor123!"),
                Role = Role.Doctor
            },
            new User 
            { 
                Email = "patient@hospital.com",
                Password = PasswordHasher.HashPassword("Patient123!"),
                Role = Role.Patient
            }
        };

        context.Users.AddRange(users);
        context.SaveChanges();
    }
}