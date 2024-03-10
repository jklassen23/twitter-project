import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { Observable, filter, map, startWith } from 'rxjs';
import { Post } from 'src/app/models/post';
import { User } from 'src/app/models/user';
import { postService } from 'src/app/services/post.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {
  username: string = "";
  currentUsername: string | null = null;
  user: any = "";
  constructor(private postService: postService,private userService: UserService, private router: Router, private route: ActivatedRoute) {this.route.params.subscribe(params => {
    this.router.events.pipe(
      filter((event) => event instanceof NavigationEnd)
    ).subscribe(() => {
      // Extract username from the current route
      const usernameFromRoute = this.route.firstChild?.snapshot.params['username'];
      this.currentUsername = usernameFromRoute || null;
    });
  }); }

  
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.username = params['username'];
      this.loadUserData();
    });
  }

  loadUserData() {
    // Fetch user details based on the username
    this.userService.getUserByUsername(this.username).subscribe(
      (user: User) => {
        console.log('User details retrieved successfully:', user);
        this.user = user;
      },
      (error) => {
        console.log('Error retrieving user details:', error);
      }
    );
  }
  editUser() {
    this.userService.updateUser(this.user).subscribe(
      (updatedUser: User) => {
        console.log('User details updated successfully:', updatedUser);
        this.router.navigateByUrl(`/user/${this.username}/post`);
      },
      (error) => {
        console.log('Error updating user details:', error);
      }
    );
  }

  
  isLoggedIn(postUsername: string | undefined): boolean {
    const token = localStorage.getItem('myPostToken');
    
    // Check if the token is present and postUsername is defined
    if (token && postUsername) {
      // Extract username from the token
      const usernameFromToken = this.extractUsernameFromToken(token);
  
      // Compare the extracted username with the username of the current post
      return usernameFromToken === postUsername;
    }
  
    return false;
  }
  extractUsernameFromToken(token: string): string {
    const decodedToken: { [key: string]: any } = this.parseJwt(token);
    return decodedToken['unique_name'] || '';
  }

  parseJwt(token: string): { [key: string]: any } {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(atob(base64).split('').map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)).join(''));
    return JSON.parse(jsonPayload);
  }
}