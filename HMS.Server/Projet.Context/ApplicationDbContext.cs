using Microsoft.EntityFrameworkCore;
using Projet.Entities;

namespace Projet.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<LabReport> LabReports { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Role enum configuration
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            // Appointment configuration
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AppointmentDate).IsRequired();
                entity.Property(e => e.Status).HasConversion<string>();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                // Relationships
                entity.HasOne(a => a.Doctor)
                    .WithMany()
                    .HasForeignKey(a => a.DoctorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Patient)
                    .WithMany()
                    .HasForeignKey(a => a.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Message configuration
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Content).IsRequired();

                // Relationships
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(m => m.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(m => m.RecipientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // LabReport configuration
            modelBuilder.Entity<LabReport>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ReportName).IsRequired();
                entity.Property(e => e.ResultDetails).IsRequired();

                // Relationships
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(l => l.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // MedicalHistory configuration
            modelBuilder.Entity<MedicalHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.HistoryDetails).IsRequired();

                // Relationships
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(m => m.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}