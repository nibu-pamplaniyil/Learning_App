import { ApplicationConfig } from '@angular/core';
import { provideRouter, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { OtpComponent } from './auth/otp/otp.component';
import { RegisterComponent } from './auth/register/register.component';
import { provideHttpClient } from '@angular/common/http';


import { provideClientHydration } from '@angular/platform-browser';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'otp', component: OtpComponent },
  { path: 'register', component: RegisterComponent }
];

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    provideClientHydration()]
};
