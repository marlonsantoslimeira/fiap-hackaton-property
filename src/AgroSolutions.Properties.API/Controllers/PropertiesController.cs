using AgroSolutions.Properties.Application.DTOs;
using AgroSolutions.Properties.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroSolutions.Properties.API.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _svc;

    public PropertiesController(IPropertyService svc)
    {
        _svc = svc;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var properties = await _svc.GetAllAsync();
        return Ok(properties);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var property = await _svc.GetByIdAsync(id);
        if (property is null)
            return NotFound();
        return Ok(property);
    }
    [HttpPost]
    public async Task<IActionResult> Create(PropertyInputDto property)
    {
        if (property is null)
            return BadRequest("Property input cannot be null.");
        await _svc.AddAsync(property);
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
