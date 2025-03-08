import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AuthService } from '../../services/auth/auth.service';
import { provideHttpClient } from '@angular/common/http';
import { ignoreElements } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-auth',
  imports: [CommonModule, FormsModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss',
})
export class AuthComponent implements OnInit {
  username!: string;
  email!: string;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    console.log('AuthComponent initialized');
  }

  login(): void {
    this.authService.login(this.username, this.email).subscribe({
      next: (response: any) => {
        console.log(response);
        localStorage.setItem('token', response.token);
        window.location.href = '';
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
