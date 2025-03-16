import { Routes } from '@angular/router';
import { ChatComponent } from './pages/chat/chat.component';
import { AuthComponent } from './pages/auth/auth.component';
import { CreateAccountComponent } from './pages/create-account/create-account.component';

const mainPath: string = localStorage.getItem('token')?.length  ? 'chat' : 'auth';

export const routes: Routes = [
    {path: '', redirectTo: mainPath, pathMatch: 'full'},
    {path: 'chat'},
    {path: 'auth', component: AuthComponent},
    {path: 'create-account', component: CreateAccountComponent}
];

