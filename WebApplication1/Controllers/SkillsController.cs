using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/skills")]
[ApiController]
public class SkillsController : Controller
{
    private readonly ISkillsService _skillsService;

    public SkillsController(ISkillsService skillsService)
    {
        _skillsService = skillsService;
    }
    [HttpPost]
    public async Task<IActionResult> AddSkills(string skills)
    {
        var result = await _skillsService.AddSkills(skills);
        if (result != null)
            return Ok(new { message = result });
        return BadRequest("Adding skill failed"); 
    }
    [HttpGet]
    public async Task<List<Skills>> GetallSkills()
    {
        try
        {
            var result = await _skillsService.GetAllSkills();
            if (result == null)
                return null;
            return result;  
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSkills(int id)
    {
        try
        {
            var result = await _skillsService.DeleteSkills(id);
            if(result == null)
                return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSkills(Guid Id, string skill)
    {
        try
        {
            var result = await _skillsService.UpdateSkills(Id, skill);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
