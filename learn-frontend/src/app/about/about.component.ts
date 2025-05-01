import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-about',
  standalone: true,
  imports: [],
  templateUrl: './about.component.html',
  styleUrl: './about.component.scss'
})
export class AboutComponent implements OnInit{
  description:string='';

  constructor(private http:HttpClient){}
  ngOnInit(): void {
    this.http.get<any>('http://localhost:5263/api/profile').subscribe(x=>{
      const data = x?.data?.[0]
      if(data)
      {
        this.description=data.description;
      }
      
    });
  }
}
