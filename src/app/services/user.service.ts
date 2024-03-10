import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { User } from '../models/user';
import { Observable, catchError, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseURL: string = "http://localhost:5235/api/Auth";
  tokenKey: string = "myPostToken";
  constructor(private http: HttpClient) { }

  signUp(newUser: User){
    return this.http.post(`${this.baseURL}/register`, newUser);
  }

  login(Username: string, Password: string){
    let queryParams = new HttpParams();
    queryParams = queryParams.append('username', Username);
    queryParams = queryParams.append('password', Password);

    return this.http.get(`${this.baseURL}/login`,  { params: queryParams, responseType: 'text' })
      .pipe(tap((response: any) => {
        localStorage.setItem('myPostToken', response);
      }));
  }

  getCurrentUser(): Observable<User>{
    let reqHeaders = {
      Authorization: `Bearer ${localStorage.getItem(this.tokenKey)}`
  }
    return this.http.get(`${this.baseURL}/current`, { headers: reqHeaders })
  }

  getUserByUsername(username: string): Observable<User>{
    return this.http.get(`${this.baseURL}/username?username=${username}`)
  }
  logout(): Observable<any> {
    localStorage.removeItem('myPostToken');
    return this.http.post(`${this.baseURL}/logout`, {});
  }
  updateUser(updatedUser: User): Observable<User> {
    return this.http.put<User>(`${this.baseURL}/update`, updatedUser);
  }
}