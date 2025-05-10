using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class SubSkillsServicer : ISubSkillsServicer
{
    private readonly ApplicationDBContext _context;

    public SubSkillsServicer(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<SubSkills> AddSubSkills(Guid Id,string name)
    {
        var skills = await _context.Skills.FindAsync(Id);
        if(skills!=null)
        {
            var subSKills = new SubSkills
            {
                SkillId = Id,
                SubSkill =name
            };
            _context.SubSkills.Add(subSKills);
            _context.SaveChanges();
            return subSKills;
        }
        return null;
    }

    public async Task<SubSkills> DeleteSubSkills(Guid Id)
    {
        var skill = await _context.SubSkills.FindAsync(Id);
        if(skill==null)
        {
            return null;
        }
        _context.SubSkills.Remove(skill);
        _context.SaveChanges();
        return skill;
    }
    public async Task<List<SubSkills>> GetSubSkills()
    {
        var subskills = await _context.SubSkills.ToListAsync();
        if(subskills==null || subskills.Count == 0)
        {
            return null;
        }
        return subskills;
    }

    public async Task<SubSkills> UpdateSubSkills(Guid id,Guid skillid, string name)
    {
        var skills = await _context.SubSkills.FindAsync(id);
        if(skills==null)
        {
            return null;
        }   
        skills.SkillId=skillid;
        skills.SubSkill=name;

        await _context.SaveChangesAsync();
        return skills;

    }
}