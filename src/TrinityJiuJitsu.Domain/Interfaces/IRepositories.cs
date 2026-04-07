using TrinityJiuJitsu.Domain.Entities;

namespace TrinityJiuJitsu.Domain.Interfaces;

public interface IGymRepository
{
    Task<Gym> CreateAsync(Gym gym);
    Task<List<Gym>> GetAllAsync();
    Task<Gym?> GetByIdAsync(Guid id);
    Task<Gym> UpdateAsync(Gym gym);
    Task<bool> DeleteAsync(Guid id);
}

public interface IBranchRepository
{
    Task<Branch> CreateAsync(Branch branch);
    Task<List<Branch>> GetByGymIdAsync(Guid gymId);
    Task<Branch?> GetByIdAsync(Guid id);
    Task<Branch> UpdateAsync(Branch branch);
    Task<bool> DeleteAsync(Guid id);
}

public interface ITrainingClassRepository
{
    Task<TrainingClass> CreateAsync(TrainingClass trainingClass);
    Task<List<TrainingClass>> GetByBranchIdAsync(Guid branchId);
    Task<TrainingClass?> GetByIdAsync(Guid id);
    Task<TrainingClass> UpdateAsync(TrainingClass trainingClass);
    Task<bool> DeleteAsync(Guid id);
}

public interface IStudentRepository
{
    Task<Student> CreateAsync(Student student);
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(Guid id);
    Task<Student> UpdateAsync(Student student);
    Task<bool> DeleteAsync(Guid id);
}

public interface IAttendanceRepository
{
    Task<Attendance> CreateAsync(Attendance attendance);
    Task<List<Attendance>> GetByClassIdAsync(Guid classId);
}
