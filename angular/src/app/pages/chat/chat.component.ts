import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { baseUrl } from '../../../global';
import { jwtDecode } from 'jwt-decode';
import { AsideComponent } from '../../components/aside/aside.component';

@Component({
  standalone: true,
  selector: 'app-chat',
  imports: [FormsModule, CommonModule, AsideComponent],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent {
  public emailTo: string = localStorage.getItem('emailTo') ?? '';
  public emailFrom: string = '';

  public message: string = '';
  public messages: string[] = [];

  private connection: HubConnection;

  constructor() {
    this.connection = new HubConnectionBuilder()
    .withUrl(baseUrl + 'chatHub', {
      accessTokenFactory: () => localStorage.getItem('token') ?? ''
    })
    .withAutomaticReconnect()
    .build()
  }

  async ngOnInit() {
    this.getMyEmail();

    console.log('Connecting...');
    this.connection.on('ReceiveMessage', (emailFrom, message) => {
      this.messages.push(`${message}`);
    });

    try {
      console.log('Connection started');
      await this.connection.start();
      console.log("Connected");

    } catch (err) {
      console.error('Erro ao iniciar conexão:', err);
    }
    
  }

  async sendMessage(emailTo: string, message: string) {
    await this.connection.invoke('SendMessage', emailTo, message);
  }

  getMyEmail() {
    const token = localStorage.getItem('token');
    if (token) {
      const decode: any = jwtDecode(token);
      return this.emailFrom = decode.email;
    } 
    
    return window.location.href = '/auth';

  }
}
