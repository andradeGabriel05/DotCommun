import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/aside/users/users.service';
import { CommonModule, NgIf } from '@angular/common';

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

  constructor(private usersServices: UsersService) {}

  ngOnInit(): void {
    this.usersServices.getUsers().subscribe((res) => {
      this.users = res;
      console.log(res);
      console.log(this.users);

    });
  }

  public setSelectUser(user: any, email: any): void {
    this.selectedUser = user;
    localStorage.setItem('emailTo', email);
  }

  public clearSelection(): void {
    this.selectedUser = null;
  }
}
