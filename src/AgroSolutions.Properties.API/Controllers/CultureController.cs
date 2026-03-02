using AgroSolutions.Properties.Application.DTOs;
using AgroSolutions.Properties.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AgroSolutions.Properties.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CultureController : ControllerBase
{
    private readonly ICultureService _svc;

    public CultureController(ICultureService svc)
    {
        _svc = svc;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cultures = await _svc.GetAllAsync();
        return Ok(cultures);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var culture = await _svc.GetByIdAsync(id);
        if (culture is null)
            return NotFound();
        return Ok(culture);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CultureInputDto culture)
    {
        if (culture is null)
            return BadRequest("Culture input cannot be null.");
        await _svc.AddAsync(culture);
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
