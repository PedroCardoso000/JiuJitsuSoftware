using TrinityJiuJitsu.Application.DTOs;

namespace TrinityJiuJitsu.Application.Interfaces;

public interface IGymService
{
    Task<GymResponse> CreateAsync(CreateGymRequest request);
    Task<List<GymResponse>> GetAllAsync();
    Task<GymResponse> UpdateAsync(Guid id, UpdateGymRequest request);
    Task DeleteAsync(Guid id);
}

public interface IBranchService
{
    Task<BranchResponse> CreateAsync(CreateBranchRequest request);
    Task<List<BranchResponse>> GetByGymIdAsync(Guid gymId);
    Task<BranchResponse> UpdateAsync(Guid id, UpdateBranchRequest request);
    Task DeleteAsync(Guid id);
}

public interface ITrainingClassService
{
    Task<ClassResponse> CreateAsync(CreateClassRequest request);
    Task<List<ClassResponse>> GetByBranchIdAsync(Guid branchId);
    Task<ClassResponse> UpdateAsync(Guid id, UpdateClassRequest request);
    Task DeleteAsync(Guid id);
}

public interface IStudentService
{
    Task<StudentResponse> CreateAsync(CreateStudentRequest request);
    Task<List<StudentResponse>> GetAllAsync();
    Task<StudentResponse> UpdateAsync(Guid id, UpdateStudentRequest request);
    Task<StudentResponse> PromoteStudentAsync(Guid id, PromoteStudentRequest request);
    Task DeleteAsync(Guid id);
}

public interface IAttendanceService
{
    Task<AttendanceResponse> CheckInAsync(CheckInRequest request);
    Task<List<AttendanceResponse>> GetByClassIdAsync(Guid classId);
}
