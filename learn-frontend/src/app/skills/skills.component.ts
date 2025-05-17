import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { SkillService } from './skill.service';
import { Skill } from './skill.model';

@Component({
  selector: 'app-skills',
  standalone: true,
  imports: [CommonModule, RouterModule], 
  templateUrl: './skills.component.html',
  styleUrls: ['./skills.component.scss']
})
export class SkillsComponent implements OnInit {
  skills: Skill[] = [];

  constructor(private skillService: SkillService, private route: ActivatedRoute) {}

    ngOnInit(): void {
  this.skillService.getSkills().subscribe((response: any) => {
    const skillsArray = response?.$values || response || [];

    this.skills = skillsArray.map((skill: any) => ({
      ...skill,
      subSkills: skill.subSkills?.$values || skill.subSkills || []
    }));
  });
}

}
