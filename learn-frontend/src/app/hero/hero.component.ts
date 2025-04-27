import { Component } from '@angular/core';

@Component({
  selector: 'app-hero',
  standalone:true,
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.scss']
})
export class HeroComponent {
  greeting: string = 'HELLO!';
  name: string = 'I Am [Your Name]';
  description: string = "I'm a [Your Profession] with [Your Experience] years of experience. My expertise is in [Your Skills] and more...";
  viewWorkLink: string = '/portfolio'; // Example link
  hireMeLink: string = '/contact';   // Example link
  profileImage: string = 'C:\Users\nibug\Downloads\WhatsApp Image 2025-04-10 at 16.25.38_2be1ae48.jpg'; // Path to your image
}