import { Injectable } from '@angular/core';
import { baseUrl } from '../../global';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  login(username: string, email: string) {
    return this.http.post(`${baseUrl}api/Auth/login`, { username, email });
  }
}
