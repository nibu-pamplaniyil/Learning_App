import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss'] // ✅ fixed plural
})
export class ContactComponent {
  name: string = '';
  email: string = '';
  subject: string = '';
  message: string = '';
  errorMessage: string = '';

  constructor(private router: Router, private httpClient: HttpClient) {}

  sendMessage() {
    const messageData = {
      Name: this.name,
      Email: this.email,
      Subject: this.subject,
      Message: this.message
    };

    this.httpClient.post('http://localhost:5263/api/contact', messageData)
      .subscribe(
        (response: any) => {
          console.log("send");
          this.errorMessage = '';
          this.router.navigate(['/']);
        },
        (error) => {
          this.errorMessage = 'not send';
          console.error('Message failed:', error);
        }
      );
  }
}
