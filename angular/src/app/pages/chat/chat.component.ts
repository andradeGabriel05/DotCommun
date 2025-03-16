import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { baseUrl } from '../../../global';
import { jwtDecode } from 'jwt-decode';
import { AsideComponent } from '../../components/aside/aside.component';
import { IChat } from '../../interfaces/chat';
import { ChatService } from '../../services/chat/chat.service';
import { SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-chat',
  imports: [FormsModule, CommonModule, AsideComponent],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.scss',
})
export class ChatComponent implements OnInit, OnChanges{
  @Input() public idEmailTo: string = '';
  // public idEmailTo: string = localStorage.getItem('idEmailTo') ?? '';
  public emailTo: string = localStorage.getItem('emailTo') ?? '';
  public emailFrom: string = '';

  public message: string = '';
  public messages: IChat[] = [];

  public users: any[] = [];
  public userid: string = localStorage.getItem('userid') ?? '';

  private connection: HubConnection;

  constructor(private chatService: ChatService) {
    this.connection = new HubConnectionBuilder()
      .withUrl(baseUrl + 'chatHub', {
        accessTokenFactory: () => localStorage.getItem('token') ?? '',
      })
      .withAutomaticReconnect()
      .build();
  }

  async ngOnInit() {
    this.getMyEmail();
    if (!this.idEmailTo) {
      this.idEmailTo = localStorage.getItem('idEmailTo') ?? '';
    }
    console.log('Connecting...');
    this.connection.on('ReceiveMessage', (emailFrom, message) => {
      const currentEmail = this.getMyEmail();

      let senderId: string;

      emailFrom === currentEmail
        ? (senderId = this.userid)
        : (senderId = localStorage.getItem('idEmailFrom') ?? '');

      this.messages.push({ text: message, user: senderId });
    });

    try {
      console.log('Connection started');
      await this.connection.start();
      console.log('Connected');
    } catch (err) {
      console.error('Erro ao iniciar conexão:', err);
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log("mudou");
    if (changes['idEmailTo'] && changes['idEmailTo'].currentValue) {
      this.test();
    }
  }

  public test(): void {
    console.log("oi dnv");
    this.messages = [];
    console.log(this.messages);
    this.chatService
      .getMessages(this.userid, this.idEmailTo)
      .subscribe((res) => {
        res.forEach((item: { content: string; idSender: string }) => {
          this.messages.push({ text: item.content, user: item.idSender });
        });
        console.log(res);
        console.log(this.messages);
      });
  }

  async sendMessage(message: string) {
    await this.connection.invoke(
      'SendMessage',
      localStorage.getItem('idEmailTo'),
      localStorage.getItem('emailTo'),
      message
    );
  }

  getMyEmail() {
    const token = localStorage.getItem('token');
    if (token) {
      const decode: any = jwtDecode(token);
      return (this.emailFrom = decode.email);
    }

    return (window.location.href = '/auth');
  }
}
