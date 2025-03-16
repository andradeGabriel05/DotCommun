import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AsideComponent } from "./components/aside/aside.component";
import { ChatComponent } from "./pages/chat/chat.component";

@Component({
  selector: 'app-root',
  imports: [AsideComponent, RouterOutlet, ChatComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'angular';
  public idEmailTo: string = '';

  onUserSelected(newId: string): void {
    this.idEmailTo = newId;
  }
}
