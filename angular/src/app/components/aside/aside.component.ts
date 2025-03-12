import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/aside/users/users.service';
import { CommonModule, NgIf } from '@angular/common';
import { jwtDecode } from 'jwt-decode';
import { ChatService } from '../../services/chat/chat.service';

@Component({
  selector: 'app-aside',
  imports: [CommonModule, NgIf],
  templateUrl: './aside.component.html',
  styleUrls: ['./aside.component.scss'],
})
export class AsideComponent implements OnInit {
  public users: any[] = [];
  public user: string = '';
  public selectedUser: any = null;

  constructor(private usersServices: UsersService, private chat: ChatService) {}

  ngOnInit(): void {
    this.usersServices.getUsers().subscribe((res) => {
      this.users = res;
      console.log(res);
      console.log(this.users);
    });
  }

  public setSelectUser(user: any, email: any, idEmailTo: any): void {
    this.selectedUser = user;
    localStorage.setItem('emailTo', email);
    localStorage.setItem('idEmailTo', idEmailTo);
    const idUserAuth = localStorage.getItem('userid');
  }

  public test(messages: any[]): void {
    this.chat
      .getMessages(
        localStorage.getItem('userid') ?? '',
        localStorage.getItem('idEmailTo') ?? ''
      )
      .subscribe((res) => {
        res.forEach((item: { content: string; idSender: string }) => {
          this.messages.push({ text: item.content, user: item.idSender });
        });
        console.log(res);
        console.log(this.messages);
      });
  }

  public clearSelection(): void {
    this.selectedUser = null;
  }
}
