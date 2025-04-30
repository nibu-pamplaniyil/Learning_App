import {  Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { OtpComponent } from './auth/otp/otp.component';
import { RegisterComponent } from './auth/register/register.component'; 
import { HeroComponent } from './hero/hero.component';
import { HeaderComponent } from './header/header.component';
import { AboutComponent } from './about/about.component';


export const routes: Routes = [
  { path: '', component: HeroComponent},
  { path: 'login', component: LoginComponent },
  { path: 'otp', component: OtpComponent },
  { path: 'register', component: RegisterComponent },
  { path:'hero',component:HeroComponent},
  { path:'header',component:HeaderComponent},
  { path:'about',component:AboutComponent},
];


