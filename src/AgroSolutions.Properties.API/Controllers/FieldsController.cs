using AgroSolutions.Properties.Application.DTOs;
using AgroSolutions.Properties.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AgroSolutions.Properties.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FieldsController : ControllerBase
{
    private readonly IFieldService _svc;

    public FieldsController(IFieldService svc)
    {
        _svc = svc;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var fields = await _svc.GetAllAsync();
        return Ok(fields);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var field = await _svc.GetByIdAsync(id);
        if (field is null)
            return NotFound();
        return Ok(field);
    }

    [HttpPost]
    public async Task<IActionResult> Create(FieldInputDto field)
    {
        if (field is null)
            return BadRequest("Field input cannot be null.");
        await _svc.AddAsync(field);
        return Created();
    }
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _svc.RemoveAsync(id);
        return NoContent();
    }

}
