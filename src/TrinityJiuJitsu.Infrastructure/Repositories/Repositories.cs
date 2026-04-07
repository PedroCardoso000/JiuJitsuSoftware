using Microsoft.EntityFrameworkCore;
using TrinityJiuJitsu.Domain.Entities;
using TrinityJiuJitsu.Domain.Interfaces;
using TrinityJiuJitsu.Infrastructure.Data;

namespace TrinityJiuJitsu.Infrastructure.Repositories;

public class GymRepository : IGymRepository
{
    private readonly AppDbContext _db;
    public GymRepository(AppDbContext db) => _db = db;

    public async Task<Gym> CreateAsync(Gym gym) { _db.Gyms.Add(gym); await _db.SaveChangesAsync(); return gym; }
    public async Task<List<Gym>> GetAllAsync() => await _db.Gyms.AsNoTracking().OrderBy(g => g.Name).ToListAsync();
    public async Task<Gym?> GetByIdAsync(Guid id) => await _db.Gyms.FindAsync(id);
    public async Task<Gym> UpdateAsync(Gym gym) { _db.Gyms.Update(gym); await _db.SaveChangesAsync(); return gym; }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var gym = await _db.Gyms.FindAsync(id);
        if (gym is null) return false;
        _db.Gyms.Remove(gym);
        await _db.SaveChangesAsync();
        return true;
    }
}

public class BranchRepository : IBranchRepository
{
    private readonly AppDbContext _db;
    public BranchRepository(AppDbContext db) => _db = db;

    public async Task<Branch> CreateAsync(Branch branch) { _db.Branches.Add(branch); await _db.SaveChangesAsync(); return branch; }
    public async Task<List<Branch>> GetByGymIdAsync(Guid gymId) =>
        await _db.Branches.AsNoTracking().Where(b => b.GymId == gymId).OrderBy(b => b.Name).ToListAsync();
    public async Task<Branch?> GetByIdAsync(Guid id) => await _db.Branches.FindAsync(id);
    public async Task<Branch> UpdateAsync(Branch branch) { _db.Branches.Update(branch); await _db.SaveChangesAsync(); return branch; }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var branch = await _db.Branches.FindAsync(id);
        if (branch is null) return false;
        _db.Branches.Remove(branch);
        await _db.SaveChangesAsync();
        return true;
    }
}

public class TrainingClassRepository : ITrainingClassRepository
{
    private readonly AppDbContext _db;
    public TrainingClassRepository(AppDbContext db) => _db = db;

    public async Task<TrainingClass> CreateAsync(TrainingClass tc) { _db.TrainingClasses.Add(tc); await _db.SaveChangesAsync(); return tc; }
    public async Task<List<TrainingClass>> GetByBranchIdAsync(Guid branchId) =>
        await _db.TrainingClasses.AsNoTracking().Where(c => c.BranchId == branchId).OrderByDescending(c => c.Date).ToListAsync();
    public async Task<TrainingClass?> GetByIdAsync(Guid id) => await _db.TrainingClasses.FindAsync(id);
    public async Task<TrainingClass> UpdateAsync(TrainingClass trainingClass) { _db.TrainingClasses.Update(trainingClass); await _db.SaveChangesAsync(); return trainingClass; }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var trainingClass = await _db.TrainingClasses.FindAsync(id);
        if (trainingClass is null) return false;
        _db.TrainingClasses.Remove(trainingClass);
        await _db.SaveChangesAsync();
        return true;
    }
}

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _db;
    public StudentRepository(AppDbContext db) => _db = db;

    public async Task<Student> CreateAsync(Student student) { _db.Students.Add(student); await _db.SaveChangesAsync(); return student; }
    public async Task<List<Student>> GetAllAsync() => await _db.Students.AsNoTracking().OrderBy(s => s.Name).ToListAsync();
    public async Task<Student?> GetByIdAsync(Guid id) => await _db.Students.FindAsync(id);
    public async Task<Student> UpdateAsync(Student student) { _db.Students.Update(student); await _db.SaveChangesAsync(); return student; }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var student = await _db.Students.FindAsync(id);
        if (student is null) return false;
        _db.Students.Remove(student);
        await _db.SaveChangesAsync();
        return true;
    }
}

public class AttendanceRepository : IAttendanceRepository
{
    private readonly AppDbContext _db;
    public AttendanceRepository(AppDbContext db) => _db = db;

    public async Task<Attendance> CreateAsync(Attendance attendance) { _db.Attendances.Add(attendance); await _db.SaveChangesAsync(); return attendance; }
    public async Task<List<Attendance>> GetByClassIdAsync(Guid classId) =>
        await _db.Attendances.AsNoTracking()
            .Include(a => a.Student)
            .Include(a => a.TrainingClass)
            .Where(a => a.ClassId == classId)
            .OrderByDescending(a => a.CheckInTime)
            .ToListAsync();
}
