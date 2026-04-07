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
    Task<List<Student>> GetStudentsWithOverdueMembershipsAsync();
}

public interface IMembershipRepository
{
    Task<Membership> CreateAsync(Membership membership);
    Task<List<Membership>> GetAllAsync();
    Task<Membership?> GetByIdAsync(Guid id);
    Task<List<Membership>> GetByStudentIdAsync(Guid studentId);
    Task<Membership> UpdateAsync(Membership membership);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsByStudentAndMonthAsync(Guid studentId, int year, int month);
    Task<int> UpdateOverdueStatusesAsync(DateTime referenceDate);
}

public interface IAttendanceRepository
{
    Task<Attendance> CreateAsync(Attendance attendance);
    Task<List<Attendance>> GetByClassIdAsync(Guid classId);
}
