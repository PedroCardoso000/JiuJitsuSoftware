namespace TrinityJiuJitsu.Domain.Entities;

public enum MembershipStatus
{
    PENDING = 0,
    PAID = 1,
    OVERDUE = 2,
    CANCELLED = 3
}

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
    public string Belt { get; set; } = "Branca";
    public int Degrees { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
}

public class Membership
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public MembershipStatus Status { get; set; } = MembershipStatus.PENDING;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Student Student { get; set; } = null!;
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
