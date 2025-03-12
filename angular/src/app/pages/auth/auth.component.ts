import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AuthService } from '../../services/auth/auth.service';
import { provideHttpClient } from '@angular/common/http';
import { ignoreElements } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

@Component({
  standalone: true,
  selector: 'app-auth',
  imports: [CommonModule, FormsModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss',
})
export class AuthComponent {
  username!: string;
  email!: string;
  userid!: any;
  constructor(private authService: AuthService) {}

  login(): void {
    this.authService.login(this.username, this.email).subscribe({
      next: (response: any) => {
        console.log(response);
        localStorage.setItem('token', response.token);
        localStorage.setItem('userid', this.getMyId());
        
        window.location.href = '';
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  getMyId() {
    const token = localStorage.getItem('token');
    if (token) {
      const decode: any = jwtDecode(token);
      return (this.userid = decode.sub);
    }

  }
}
