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
    Task<StudentPaymentStatusResponse> IsActiveAndPaidAsync(Guid id);
    Task<List<StudentResponse>> GetStudentsWithOverdueMembershipsAsync();
    Task DeleteAsync(Guid id);
}

public interface IMembershipService
{
    Task<MembershipResponse> CreateAsync(CreateMembershipRequest request);
    Task<List<MembershipResponse>> GetAllAsync();
    Task<MembershipResponse> GetByIdAsync(Guid id);
    Task<List<MembershipResponse>> GetByStudentIdAsync(Guid studentId);
    Task<MembershipResponse> UpdateAsync(Guid id, UpdateMembershipRequest request);
    Task DeleteAsync(Guid id);
    Task<int> GenerateCurrentMonthForActiveStudentsAsync(GenerateMonthlyMembershipsRequest request);
    Task<int> RefreshOverdueStatusesAsync();
}

public interface IAttendanceService
{
    Task<AttendanceResponse> CheckInAsync(CheckInRequest request);
    Task<List<AttendanceResponse>> GetByClassIdAsync(Guid classId);
}
