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
    public DbSet<Membership> Memberships => Set<Membership>();
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
            e.Property(x => x.Belt).IsRequired().HasMaxLength(20);
            e.Property(x => x.Degrees).IsRequired();
            e.Property(x => x.IsActive).IsRequired();
        });

        modelBuilder.Entity<Membership>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Amount).HasColumnType("decimal(18,2)").IsRequired();
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
            e.HasOne(x => x.Student)
                .WithMany(s => s.Memberships)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasIndex(x => new { x.StudentId, x.DueDate });
        });

        modelBuilder.Entity<Attendance>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Student).WithMany(s => s.Attendances).HasForeignKey(x => x.StudentId);
            e.HasOne(x => x.TrainingClass).WithMany(c => c.Attendances).HasForeignKey(x => x.ClassId);
        });
    }
}
