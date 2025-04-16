import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-otp',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './otp.component.html',
  styleUrl: './otp.component.scss'
})
export class OtpComponent {
  otp: string = '';
  successMessage: string = '';
  constructor(private router: Router, private http: HttpClient) { }

  verifyOtp() {
    const OTP = {
      OTP: this.otp
    };
    this.http.post('https://localhost:7033/api/auth/otpverify', OTP)
      .subscribe(
        (response:any) => {
          console.log('OTP verified successfully', response.Message);
          this.successMessage = 'OTP verified successfully';
          // Navigate to the home page or any other page after successful OTP verification
          this.router.navigate(['/register']);
        },
        (error) => {
          console.error('OTP verification failed:', error);
          this.successMessage = 'Invalid OTP';
        }
      )
    console.log('OTP verification clicked');

   
  }
}
