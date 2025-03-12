import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { baseUrl } from '../../../global';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private http: HttpClient) { }

  getMessages(idSender: any, idReceiver: any): Observable<any> {
    return this.http.get(baseUrl+'api/Message?idSender='+idSender+'&idReceiver='+idReceiver+"&pageNumber=1&pageSize=5")
  }
}
