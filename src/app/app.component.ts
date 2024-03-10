import { Component } from '@angular/core';
import { UserService } from './services/user.service';
import { Router } from '@angular/router';
import { User } from './models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title(title: any) {
    throw new Error('Method not implemented.');
  }
  user: User = new User;
  constructor(private userService: UserService, private router: Router) {}

  logout() {
    this.userService.logout().subscribe(
      () => {
        this.router.navigate(['login']);
      },
      (error) => {
        console.error('Logout error:', error);
      }
    );
  }
  isLoggedIn(): boolean {
    return !!localStorage.getItem('myPostToken');
  }
  getProfile() {
    this.userService.getCurrentUser().subscribe(
      (user: User) => {
        if (user && user.username) {
          this.userService.getUserByUsername(user.username).subscribe(
            (userByUsername: User) => {
              if (userByUsername) {
                this.router.navigate(['user', userByUsername.username, 'post']);
              } else {
                console.error('User not found');
                // Handle the case where the user is not found (display a message, etc.)
              }
            },
            (error) => {
              console.error('Get user by username error:', error);
            }
          );
        } else {
          console.error('Current user not found');
          // Handle the case where the current user is not found (display a message, etc.)
        }
      },
      (error) => {
        console.error('Get profile error:', error);
      }
    );
  }
}
