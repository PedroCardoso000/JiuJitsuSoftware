using Microsoft.AspNetCore.Mvc;
using TrinityJiuJitsu.Application.DTOs;
using TrinityJiuJitsu.Application.Interfaces;

namespace TrinityJiuJitsu.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GymsController : ControllerBase
{
    private readonly IGymService _service;
    public GymsController(IGymService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create(CreateGymRequest request)
    {
        var result = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateGymRequest request)
    {
        try { return Ok(await _service.UpdateAsync(id, request)); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try { await _service.DeleteAsync(id); return NoContent(); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }
}

[ApiController]
[Route("api/[controller]")]
public class BranchesController : ControllerBase
{
    private readonly IBranchService _service;
    public BranchesController(IBranchService service) => _service = service;

    [HttpGet("by-gym/{gymId:guid}")]
    public async Task<IActionResult> GetByGym(Guid gymId) => Ok(await _service.GetByGymIdAsync(gymId));

    [HttpPost]
    public async Task<IActionResult> Create(CreateBranchRequest request)
    {
        try { return CreatedAtAction(nameof(GetByGym), new { gymId = request.GymId }, await _service.CreateAsync(request)); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateBranchRequest request)
    {
        try { return Ok(await _service.UpdateAsync(id, request)); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try { await _service.DeleteAsync(id); return NoContent(); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }
}

[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly ITrainingClassService _service;
    public ClassesController(ITrainingClassService service) => _service = service;

    [HttpGet("by-branch/{branchId:guid}")]
    public async Task<IActionResult> GetByBranch(Guid branchId) => Ok(await _service.GetByBranchIdAsync(branchId));

    [HttpPost]
    public async Task<IActionResult> Create(CreateClassRequest request) =>
        CreatedAtAction(nameof(GetByBranch), new { branchId = request.BranchId }, await _service.CreateAsync(request));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateClassRequest request)
    {
        try { return Ok(await _service.UpdateAsync(id, request)); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try { await _service.DeleteAsync(id); return NoContent(); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }
}

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;
    public StudentsController(IStudentService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("overdue")]
    public async Task<IActionResult> GetStudentsWithOverdueMemberships() => Ok(await _service.GetStudentsWithOverdueMembershipsAsync());

    [HttpGet("{id:guid}/is-active-and-paid")]
    public async Task<IActionResult> IsActiveAndPaid(Guid id)
    {
        try { return Ok(await _service.IsActiveAndPaidAsync(id)); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentRequest request)
    {
        var result = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateStudentRequest request)
    {
        try { return Ok(await _service.UpdateAsync(id, request)); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }

    [HttpPost("{id:guid}/promote")]
    public async Task<IActionResult> Promote(Guid id, PromoteStudentRequest request)
    {
        try { return Ok(await _service.PromoteStudentAsync(id, request)); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
        catch (InvalidOperationException ex) { return BadRequest(new { error = ex.Message }); }
        catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try { await _service.DeleteAsync(id); return NoContent(); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }
}

[ApiController]
[Route("api/[controller]")]
public class MembershipsController : ControllerBase
{
    private readonly IMembershipService _service;

    public MembershipsController(IMembershipService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try { return Ok(await _service.GetByIdAsync(id)); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }

    [HttpGet("by-student/{studentId:guid}")]
    public async Task<IActionResult> GetByStudent(Guid studentId) => Ok(await _service.GetByStudentIdAsync(studentId));

    [HttpPost]
    public async Task<IActionResult> Create(CreateMembershipRequest request)
    {
        try
        {
            var result = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateMembershipRequest request)
    {
        try { return Ok(await _service.UpdateAsync(id, request)); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try { await _service.DeleteAsync(id); return NoContent(); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }

    [HttpPost("generate-current-month")]
    public async Task<IActionResult> GenerateCurrentMonth(GenerateMonthlyMembershipsRequest request)
    {
        var generated = await _service.GenerateCurrentMonthForActiveStudentsAsync(request);
        return Ok(new { generated });
    }

    [HttpPost("refresh-overdue")]
    public async Task<IActionResult> RefreshOverdue()
    {
        var updated = await _service.RefreshOverdueStatusesAsync();
        return Ok(new { updated });
    }
}

[ApiController]
[Route("api/[controller]")]
public class AttendancesController : ControllerBase
{
    private readonly IAttendanceService _service;
    public AttendancesController(IAttendanceService service) => _service = service;

    [HttpGet("by-class/{classId:guid}")]
    public async Task<IActionResult> GetByClass(Guid classId) => Ok(await _service.GetByClassIdAsync(classId));

    [HttpPost("check-in")]
    public async Task<IActionResult> CheckIn(CheckInRequest request)
    {
        try { return CreatedAtAction(nameof(GetByClass), new { classId = request.ClassId }, await _service.CheckInAsync(request)); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }
}
