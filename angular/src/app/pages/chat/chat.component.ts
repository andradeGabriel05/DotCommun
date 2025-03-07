import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { baseUrl } from '../../../global';

@Component({
  standalone: true,
  selector: 'app-chat',
  imports: [FormsModule, CommonModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss'
})
export class ChatComponent {
  public emailTo: string = '';

  public message: string = '';
  public messages: string[] = [];

  private connection: HubConnection;

  constructor() {
    this.connection = new HubConnectionBuilder()
    .withUrl(baseUrl+'chatHub', {
      accessTokenFactory: () => localStorage.getItem('token') ?? ''
    })
    .withAutomaticReconnect()
    .build()
  }

  async ngOnInit() {

    this.connection.on('ReceiveMessage', (emailTo, message) => {
      this.messages.push(`${message}`);
    });

    try {
      await this.connection.start();
      console.log('Connection started');

    } catch (err) {
      console.error('Erro ao iniciar conexão:', err);
    }
    
  }

  async sendMessage(emailTo: string, message: string) {
    await this.connection.invoke('SendMessage', emailTo, message);
  }
}
