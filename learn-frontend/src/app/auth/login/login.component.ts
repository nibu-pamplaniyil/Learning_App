import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string='';
  constructor(private router: Router, private httpClient: HttpClient) { }
  gotoLogin() {
    const loginData = {
      UserName: this.email,
      Password: this.password
    };
    this.httpClient.post('https://localhost:7033/api/auth/login', loginData, {
      withCredentials: true
    })
      .subscribe(
        (response:any) => {
          console.log('OTP Send', response.Message);
          // Navigate to the OTP page
          this.errorMessage = '';
          this.router.navigate(['/otp'], { state: { message: response.message } });
        },
        (error) => {
          this.errorMessage = 'Invalid Credentials';
          console.error('Login failed:', error);
          // Handle login failure
        }
      );
  }
}
