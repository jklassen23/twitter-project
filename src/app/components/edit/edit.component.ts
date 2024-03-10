import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/models/post';
import { postService } from 'src/app/services/post.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  Post: any = "";

  constructor(private PostService: postService,private actRoute: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const id = this.actRoute.snapshot.params['id'];

    this.PostService.getPostByID(id).subscribe(result => {
      this.Post = result;
    })
  }

  updatePost(){
    this.PostService.updatePost(this.Post).subscribe(() => {
      window.alert("Updated Post Successfully");
        this.router.navigateByUrl("/post")
    }, error => {
      console.log('Error: ', error)
      if (error.status === 401 || error.status === 403) {
        this.router.navigate(['signin']);
      }
    });
  }
  
}