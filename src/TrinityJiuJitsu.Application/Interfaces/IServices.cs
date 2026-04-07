using TrinityJiuJitsu.Application.DTOs;

namespace TrinityJiuJitsu.Application.Interfaces;

public interface IGymService
{
    Task<GymResponse> CreateAsync(CreateGymRequest request);
    Task<List<GymResponse>> GetAllAsync();
}

public interface IBranchService
{
    Task<BranchResponse> CreateAsync(CreateBranchRequest request);
    Task<List<BranchResponse>> GetByGymIdAsync(Guid gymId);
}

public interface ITrainingClassService
{
    Task<ClassResponse> CreateAsync(CreateClassRequest request);
    Task<List<ClassResponse>> GetByBranchIdAsync(Guid branchId);
}

public interface IStudentService
{
    Task<StudentResponse> CreateAsync(CreateStudentRequest request);
    Task<List<StudentResponse>> GetAllAsync();
}

public interface IAttendanceService
{
    Task<AttendanceResponse> CheckInAsync(CheckInRequest request);
    Task<List<AttendanceResponse>> GetByClassIdAsync(Guid classId);
}
