export interface SubSkill {
  subSkill: string;
}

export interface Skill {
  id: string;
  skillName: string;
  subSkills: SubSkill[];
}