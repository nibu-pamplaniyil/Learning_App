using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class SubSkills
{
    [Key]
    public Guid Id{get; set;}
    
    [ForeignKey("Skill")]
    public Guid SkillId{get; set;}
    public string SubSkill{get; set;}

    public Skills skills{get; set;}
}