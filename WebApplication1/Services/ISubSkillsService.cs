using WebApplication1.Models;

namespace WebApplication1.Services;

public interface ISubSkillsServicer
{
    public Task<SubSkills> AddSubSkills(Guid SkillId,string Name);
    public Task<SubSkills> DeleteSubSkills(Guid Id);
    public Task<List<SubSkills>> GetSubSkills();
    public Task<SubSkills> UpdateSubSkills(Guid Id,Guid SkillId,string Name);
}