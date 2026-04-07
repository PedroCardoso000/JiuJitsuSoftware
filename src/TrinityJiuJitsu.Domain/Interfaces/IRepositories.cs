using TrinityJiuJitsu.Domain.Entities;

namespace TrinityJiuJitsu.Domain.Interfaces;

public interface IGymRepository
{
    Task<Gym> CreateAsync(Gym gym);
    Task<List<Gym>> GetAllAsync();
    Task<Gym?> GetByIdAsync(Guid id);
}

public interface IBranchRepository
{
    Task<Branch> CreateAsync(Branch branch);
    Task<List<Branch>> GetByGymIdAsync(Guid gymId);
}

public interface ITrainingClassRepository
{
    Task<TrainingClass> CreateAsync(TrainingClass trainingClass);
    Task<List<TrainingClass>> GetByBranchIdAsync(Guid branchId);
    Task<TrainingClass?> GetByIdAsync(Guid id);
}

public interface IStudentRepository
{
    Task<Student> CreateAsync(Student student);
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(Guid id);
}

public interface IAttendanceRepository
{
    Task<Attendance> CreateAsync(Attendance attendance);
    Task<List<Attendance>> GetByClassIdAsync(Guid classId);
}
