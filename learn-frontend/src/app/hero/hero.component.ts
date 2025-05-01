import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-hero',
  standalone:true,
  imports:[RouterModule],
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.scss']
})
export class HeroComponent implements OnInit{
  greeting: string = 'HELLO!';
  name: string = 'I Am Nibu George';
  experience:number=0;
  descriptions: string = '';
  designation:string='';
  viewWorkLink: string = '/portfolio'; 
  hireMeLink: string = '/contact';   
  profileImage: string = '';

  constructor(private http:HttpClient){}

  ngOnInit(): void {
    this.http.get<any>('http://localhost:5263/api/profile').subscribe(x=>{

      const data = x?.data?.[0];
      if(data)
      {
        this.experience=data.experience;
        this.profileImage=data.imageURL;
        this.designation=data.designation;
        this.descriptions = `I'm a ${this.designation} with ${this.experience} years of experience. My expertise is in web development and more...`;
      }
    });
  }
}