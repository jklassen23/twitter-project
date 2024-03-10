import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Post } from 'src/app/models/post';
import { postService } from 'src/app/services/post.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{
  postList: Post[] = [];
  newPost: Post = new Post();
  constructor(private postService: postService, private router: Router) { }

  ngOnInit(): void {
    this.postService.getAllPost().subscribe((Post: Post[]) => {
      this.postList = Post;
      console.log(Post);
    });
  }
  navigateToUserPosts(username: string | undefined): void {
    console.log('Navigating to user posts for username:', username);
    if (username) {
      this.router.navigate(['user', username, 'post']);
    } else {
      console.error('Username is undefined');
    }
  }
  onButtonClick(username: string): void {
    console.log(`Button clicked for user: ${username}`);
  }  
  addPost() {
    this.postService.createPost(this.newPost).subscribe(() => {
      window.alert("Created Post Successfully");
      location.reload();
    }, error => {
      console.log('Error: ', error)
      if (error.status === 401 || error.status === 403) {
        this.router.navigate(['signin']);
      }
    });
  }
  isLoggedIn(): boolean {
    const token = localStorage.getItem('myPostToken');
    
    return !!token;
  }
}