using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/SubSkills")]
[ApiController]
public class SubSkillsController : Controller
{
    private readonly ISubSkillsServicer _subskillservicer;
    public SubSkillsController(ISubSkillsServicer subSkillsServicer)
    {
        _subskillservicer = subSkillsServicer;
    }
    [HttpPost]
    public async Task<IActionResult> AddSubskill(Guid skillId, string subskill)
    {
        var result = await _subskillservicer.AddSubSkills(skillId, subskill);
        if (result != null)
        {
            return Ok(new { message = result });
        }
        return BadRequest();
    }

    [HttpGet]
    public async Task<List<SubSkills>> GetSubSkills()
    {
        var result = await _subskillservicer.GetSubSkills();
        if (result != null)
        {
            return result;
        }
        return null;
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteSubSkills(Guid Id)
    {
        var result = await _subskillservicer.DeleteSubSkills(Id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateSubSkills(Guid subId, Guid SkillId, string name)
    {
        var result = _subskillservicer.UpdateSubSkills(subId, SkillId, name);
        if (result == null)
            return NotFound();
        return Ok(result);
    }
}