using TrinityJiuJitsu.Application.DTOs;
using TrinityJiuJitsu.Application.Interfaces;
using TrinityJiuJitsu.Domain.Entities;
using TrinityJiuJitsu.Domain.Interfaces;

namespace TrinityJiuJitsu.Application.Services;

public class GymService : IGymService
{
    private readonly IGymRepository _repo;
    public GymService(IGymRepository repo) => _repo = repo;

    public async Task<GymResponse> CreateAsync(CreateGymRequest request)
    {
        var gym = new Gym { Id = Guid.NewGuid(), Name = request.Name };
        await _repo.CreateAsync(gym);
        return new GymResponse(gym.Id, gym.Name, gym.CreatedAt);
    }

    public async Task<List<GymResponse>> GetAllAsync()
    {
        var gyms = await _repo.GetAllAsync();
        return gyms.Select(g => new GymResponse(g.Id, g.Name, g.CreatedAt)).ToList();
    }

    public async Task<GymResponse> UpdateAsync(Guid id, UpdateGymRequest request)
    {
        var gym = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Gym {id} not found.");
        gym.Name = request.Name;
        await _repo.UpdateAsync(gym);
        return new GymResponse(gym.Id, gym.Name, gym.CreatedAt);
    }

    public async Task DeleteAsync(Guid id)
    {
        var deleted = await _repo.DeleteAsync(id);
        if (!deleted) throw new KeyNotFoundException($"Gym {id} not found.");
    }
}

public class BranchService : IBranchService
{
    private readonly IBranchRepository _repo;
    private readonly IGymRepository _gymRepo;
    public BranchService(IBranchRepository repo, IGymRepository gymRepo) { _repo = repo; _gymRepo = gymRepo; }

    public async Task<BranchResponse> CreateAsync(CreateBranchRequest request)
    {
        var gym = await _gymRepo.GetByIdAsync(request.GymId)
            ?? throw new KeyNotFoundException($"Gym {request.GymId} not found.");

        var branch = new Branch { Id = Guid.NewGuid(), Name = request.Name, GymId = request.GymId };
        await _repo.CreateAsync(branch);
        return new BranchResponse(branch.Id, branch.Name, branch.GymId, branch.CreatedAt);
    }

    public async Task<List<BranchResponse>> GetByGymIdAsync(Guid gymId)
    {
        var list = await _repo.GetByGymIdAsync(gymId);
        return list.Select(b => new BranchResponse(b.Id, b.Name, b.GymId, b.CreatedAt)).ToList();
    }

    public async Task<BranchResponse> UpdateAsync(Guid id, UpdateBranchRequest request)
    {
        var branch = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Branch {id} not found.");

        var gym = await _gymRepo.GetByIdAsync(request.GymId)
            ?? throw new KeyNotFoundException($"Gym {request.GymId} not found.");

        branch.Name = request.Name;
        branch.GymId = gym.Id;
        await _repo.UpdateAsync(branch);
        return new BranchResponse(branch.Id, branch.Name, branch.GymId, branch.CreatedAt);
    }

    public async Task DeleteAsync(Guid id)
    {
        var deleted = await _repo.DeleteAsync(id);
        if (!deleted) throw new KeyNotFoundException($"Branch {id} not found.");
    }
}

public class TrainingClassService : ITrainingClassService
{
    private readonly ITrainingClassRepository _repo;
    private readonly IBranchRepository _branchRepo;
    public TrainingClassService(ITrainingClassRepository repo, IBranchRepository branchRepo) { _repo = repo; _branchRepo = branchRepo; }

    public async Task<ClassResponse> CreateAsync(CreateClassRequest request)
    {
        var tc = new TrainingClass
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Date = request.Date,
            BranchId = request.BranchId
        };
        await _repo.CreateAsync(tc);
        return new ClassResponse(tc.Id, tc.Name, tc.Date, tc.BranchId, tc.CreatedAt);
    }

    public async Task<List<ClassResponse>> GetByBranchIdAsync(Guid branchId)
    {
        var list = await _repo.GetByBranchIdAsync(branchId);
        return list.Select(c => new ClassResponse(c.Id, c.Name, c.Date, c.BranchId, c.CreatedAt)).ToList();
    }

    public async Task<ClassResponse> UpdateAsync(Guid id, UpdateClassRequest request)
    {
        var trainingClass = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Class {id} not found.");
        var branch = await _branchRepo.GetByIdAsync(request.BranchId)
            ?? throw new KeyNotFoundException($"Branch {request.BranchId} not found.");

        trainingClass.Name = request.Name;
        trainingClass.Date = request.Date;
        trainingClass.BranchId = branch.Id;
        await _repo.UpdateAsync(trainingClass);
        return new ClassResponse(trainingClass.Id, trainingClass.Name, trainingClass.Date, trainingClass.BranchId, trainingClass.CreatedAt);
    }

    public async Task DeleteAsync(Guid id)
    {
        var deleted = await _repo.DeleteAsync(id);
        if (!deleted) throw new KeyNotFoundException($"Class {id} not found.");
    }
}

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repo;
    public StudentService(IStudentRepository repo) => _repo = repo;

    public async Task<StudentResponse> CreateAsync(CreateStudentRequest request)
    {
        var student = new Student { Id = Guid.NewGuid(), Name = request.Name };
        await _repo.CreateAsync(student);
        return new StudentResponse(student.Id, student.Name, student.CreatedAt);
    }

    public async Task<List<StudentResponse>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.Select(s => new StudentResponse(s.Id, s.Name, s.CreatedAt)).ToList();
    }

    public async Task<StudentResponse> UpdateAsync(Guid id, UpdateStudentRequest request)
    {
        var student = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Student {id} not found.");
        student.Name = request.Name;
        await _repo.UpdateAsync(student);
        return new StudentResponse(student.Id, student.Name, student.CreatedAt);
    }

    public async Task DeleteAsync(Guid id)
    {
        var deleted = await _repo.DeleteAsync(id);
        if (!deleted) throw new KeyNotFoundException($"Student {id} not found.");
    }
}

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _repo;
    private readonly IStudentRepository _studentRepo;
    private readonly ITrainingClassRepository _classRepo;

    public AttendanceService(IAttendanceRepository repo, IStudentRepository studentRepo, ITrainingClassRepository classRepo)
    {
        _repo = repo;
        _studentRepo = studentRepo;
        _classRepo = classRepo;
    }

    public async Task<AttendanceResponse> CheckInAsync(CheckInRequest request)
    {
        var student = await _studentRepo.GetByIdAsync(request.StudentId)
            ?? throw new KeyNotFoundException($"Student {request.StudentId} not found.");
        var trainingClass = await _classRepo.GetByIdAsync(request.ClassId)
            ?? throw new KeyNotFoundException($"Class {request.ClassId} not found.");

        var attendance = new Attendance
        {
            Id = Guid.NewGuid(),
            StudentId = request.StudentId,
            ClassId = request.ClassId
        };
        await _repo.CreateAsync(attendance);

        return new AttendanceResponse(attendance.Id, student.Id, student.Name, trainingClass.Id, trainingClass.Name, attendance.CheckInTime);
    }

    public async Task<List<AttendanceResponse>> GetByClassIdAsync(Guid classId)
    {
        var list = await _repo.GetByClassIdAsync(classId);
        return list.Select(a => new AttendanceResponse(
            a.Id, a.StudentId, a.Student.Name, a.ClassId, a.TrainingClass.Name, a.CheckInTime
        )).ToList();
    }
}
