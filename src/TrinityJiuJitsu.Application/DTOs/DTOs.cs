namespace TrinityJiuJitsu.Application.DTOs;

// === Requests ===
public record CreateGymRequest(string Name);
public record UpdateGymRequest(string Name);
public record CreateBranchRequest(string Name, Guid GymId);
public record UpdateBranchRequest(string Name, Guid GymId);
public record CreateClassRequest(string Name, DateTime Date, Guid BranchId);
public record UpdateClassRequest(string Name, DateTime Date, Guid BranchId);
public record CreateStudentRequest(string Name);
public record UpdateStudentRequest(string Name);
public record CheckInRequest(Guid StudentId, Guid ClassId);

// === Responses ===
public record GymResponse(Guid Id, string Name, DateTime CreatedAt);
public record BranchResponse(Guid Id, string Name, Guid GymId, DateTime CreatedAt);
public record ClassResponse(Guid Id, string Name, DateTime Date, Guid BranchId, DateTime CreatedAt);
public record StudentResponse(Guid Id, string Name, DateTime CreatedAt);
public record AttendanceResponse(Guid Id, Guid StudentId, string StudentName, Guid ClassId, string ClassName, DateTime CheckInTime);
