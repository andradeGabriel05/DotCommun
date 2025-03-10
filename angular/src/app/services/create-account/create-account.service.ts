import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { baseUrl } from '../../../global';

@Injectable({
  providedIn: 'root'
})
export class CreateAccountService {

  constructor(private http: HttpClient) { }

  createAccount(username: string, email: string): void {
    console.log('Creating account...');
    console.log(`Username: ${username}`);
    console.log(`Email: ${email}`);

    this.http.post(baseUrl+'api/User', { username, email }).subscribe({
      next: (response: any) => {
        console.log(response);
        window.location.href = '/auth';
      },
      error: (error) => {
        console.log(error);
      },
    });

  }
}
