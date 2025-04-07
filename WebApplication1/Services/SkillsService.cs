using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class SkillsService : ISkillsService
{
    private readonly ApplicationDBContext _context;

    public SkillsService(ApplicationDBContext context)
    {
        _context = context;
    }
    public async Task<Skills> AddSkills(string skill)
    {
        var skills = new Skills
        {
            SkillName = skill
        };
        _context.Skills.Add(skills);
        _context.SaveChanges();
        return skills;
    }

    public async Task<List<Skills>> GetAllSkills()
    {
        var skills = await _context.Skills.ToListAsync();
        return skills;
    }

    public async Task<Skills> DeleteSkills(int id)
    {
        var skill = await _context.Skills.FindAsync(id);
        if(skill == null)
        {
            return null;
        }
        _context.Skills.Remove(skill);
        _context.SaveChanges();
        return skill;
    }

    public async Task<Skills> UpdateSkills(Guid id, string skill)
    {
        var skills = await _context.Skills.FindAsync(id);
        if(skills == null)
        {
            return null;
        }
        skills.SkillName = skill;
        _context.Skills.Update(skills);
        _context.SaveChanges();
        return skills;
    }
}
