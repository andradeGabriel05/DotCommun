import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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
    @Input() idEmailTo!: string;
    @Output() userSelected = new EventEmitter<any>();

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

    this.userSelected.emit(idEmailTo);
    console.log("Oi")
  }



  public clearSelection(): void {
    this.selectedUser = null;
  }
}
