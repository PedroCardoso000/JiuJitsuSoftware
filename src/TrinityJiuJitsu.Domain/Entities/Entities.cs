namespace TrinityJiuJitsu.Domain.Entities;

public class Gym
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Branch> Branches { get; set; } = new List<Branch>();
}

public class Branch
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid GymId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Gym Gym { get; set; } = null!;
    public ICollection<TrainingClass> Classes { get; set; } = new List<TrainingClass>();
}

public class TrainingClass
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public Guid BranchId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Branch Branch { get; set; } = null!;
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
}

public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
}

public class Attendance
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid ClassId { get; set; }
    public DateTime CheckInTime { get; set; } = DateTime.UtcNow;

    public Student Student { get; set; } = null!;
    public TrainingClass TrainingClass { get; set; } = null!;
}
