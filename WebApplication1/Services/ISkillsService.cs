using WebApplication1.Models;

namespace WebApplication1.Services;

public interface ISkillsService
{
    public Task<Skills> AddSkills(string skill);
    public Task<List<Skills>> GetAllSkills();
    public Task<Skills> DeleteSkills(int id);
    public Task<Skills> UpdateSkills(Guid id, string skill);
}
