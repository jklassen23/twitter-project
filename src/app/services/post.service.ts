import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Post } from '../models/post';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class postService {
  baseURL: string = "http://localhost:5235/api/Post";
  tokenKey: string = "myPostToken";

  constructor(private http: HttpClient) { }

  getAllPost(): Observable<Post[]> {
    return this.http.get<Post[]>(this.baseURL);
  }

  getPostByID(postId: string): Observable<Post> {
    return this.http.get<Post>(`${this.baseURL}/${postId}`)
  }

  createPost(newpost: Post) {
    let reqHeaders = {
        Authorization: `Bearer ${localStorage.getItem(this.tokenKey)}`
    }
    return this.http.post(this.baseURL, newpost, { headers: reqHeaders });
  }

  updatePost(updatedpost: Post) {
    let reqHeaders = {
      Authorization: `Bearer ${localStorage.getItem(this.tokenKey)}`
  }
  return this.http.put(`${this.baseURL}/${updatedpost.postId}`, updatedpost, { headers: reqHeaders });
  }

  deletePost(postId: string): Observable<Post>{
    let reqHeaders = {
      Authorization: `Bearer ${localStorage.getItem(this.tokenKey)}`
  }
    return this.http.delete(`${this.baseURL}/${postId}`, { headers: reqHeaders })
  }
  getUsersPosts(username: string): Observable<Post[]> {
    return this.http.get<Post[]>(`${this.baseURL}/${username}`);
  }
}