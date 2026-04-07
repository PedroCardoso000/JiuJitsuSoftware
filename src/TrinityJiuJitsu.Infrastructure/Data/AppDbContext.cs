using Microsoft.EntityFrameworkCore;
using TrinityJiuJitsu.Domain.Entities;

namespace TrinityJiuJitsu.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Gym> Gyms => Set<Gym>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<TrainingClass> TrainingClasses => Set<TrainingClass>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Attendance> Attendances => Set<Attendance>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gym>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Branch>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasOne(x => x.Gym).WithMany(g => g.Branches).HasForeignKey(x => x.GymId);
        });

        modelBuilder.Entity<TrainingClass>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.HasOne(x => x.Branch).WithMany(b => b.Classes).HasForeignKey(x => x.BranchId);
        });

        modelBuilder.Entity<Student>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Attendance>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Student).WithMany(s => s.Attendances).HasForeignKey(x => x.StudentId);
            e.HasOne(x => x.TrainingClass).WithMany(c => c.Attendances).HasForeignKey(x => x.ClassId);
        });

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var gymId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var branchId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var classId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var studentId = Guid.Parse("44444444-4444-4444-4444-444444444444");

        modelBuilder.Entity<Gym>().HasData(new Gym
        {
            Id = gymId,
            Name = "Trinity IBF",
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });

        modelBuilder.Entity<Branch>().HasData(new Branch
        {
            Id = branchId,
            Name = "Sede Fortaleza",
            GymId = gymId,
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });

        modelBuilder.Entity<TrainingClass>().HasData(new TrainingClass
        {
            Id = classId,
            Name = "Fundamentos - Segunda 19h",
            Date = new DateTime(2025, 4, 7, 19, 0, 0, DateTimeKind.Utc),
            BranchId = branchId,
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });

        modelBuilder.Entity<Student>().HasData(new Student
        {
            Id = studentId,
            Name = "Alan (Faixa Azul)",
            CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });
    }
}
