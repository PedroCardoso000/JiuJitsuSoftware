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
