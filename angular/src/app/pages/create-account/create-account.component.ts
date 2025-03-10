import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CreateAccountService } from '../../services/create-account/create-account.service';

@Component({
  standalone: true,
  selector: 'app-create-account',
  imports: [CommonModule, FormsModule],
  templateUrl: './create-account.component.html',
  styleUrl: './create-account.component.scss',
})
export class CreateAccountComponent {
  constructor(private createAccountService: CreateAccountService) {}
  username!: string;
  email!: string;

  createAccount(): void {
    this.createAccountService.createAccount(this.username, this.email);
  }
}
