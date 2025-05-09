import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { baseUrl } from '../../../../global';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  constructor(private http: HttpClient) {}

  getUsers(): Observable<any> {
    return this.http.get<any>(baseUrl+'api/User')
  }

  getUsersChat(idSender: any, idReceiver: any): Observable<any> {
    return this.http.get<any>(baseUrl+'api/Message?idSender='+idSender+'&idReceiver='+idReceiver+"&pageNumber=1&pageSize=5")
  }
}
