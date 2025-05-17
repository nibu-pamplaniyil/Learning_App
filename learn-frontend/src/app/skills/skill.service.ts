import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Skill } from './skill.model';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SkillService {
  private apiUrl = 'http://localhost:5263/api/skills'; 

  constructor(private http: HttpClient) { }

  getSkills(): Observable<Skill[]> {
    return this.http.get<any>(this.apiUrl).pipe(
      map(response => {
        const skillsArray = response?.$values || [];
        return skillsArray.map((skill: any) => ({
          ...skill,
          subSkills: skill.subSkills?.$values || []
        }));
      })
    );
  }
}
