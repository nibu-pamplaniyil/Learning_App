import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SafeUrlModule } from './safe-url.module';
import { HeroService } from './hero.service';

@Component({
  selector: 'app-hero',
  standalone:true,
  imports:[RouterModule,CommonModule,SafeUrlModule],
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.scss']
})
export class HeroComponent implements OnInit {
  greeting: string = 'HELLO!';
  name: string = 'I Am Nibu George';
  experience: number = 0;
  descriptions: string = '';
  resume: string = '';
  designation: string = '';
  viewWorkLink: string = '/portfolio'; 
  hireMeLink: string = '/contact';   
  profileImage: string = '';
  resumeVisible = false;

  constructor(private heroServicer:HeroService) {}

  ngOnInit(): void {
    this.heroServicer.getProfile().subscribe(data => {
      if (data) {
        this.experience = data.experience;
        this.profileImage = data.imageURL;
        this.designation = data.designation;
        this.resume = data.resume;
        this.descriptions = `I'm a ${this.designation} with ${this.experience} years of experience. My expertise is in web development and more...`;
      }
    });
  }
  

  viewResume(): void {
    if (this.resume) {
      this.resumeVisible = !this.resumeVisible;
    } else {
      alert('Resume not available');
    }
  }
}
