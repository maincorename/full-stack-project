import { ILoginData } from './../models/loginData';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ConfigurationService } from './configuration.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userPath!: string;

  constructor(
    private http: HttpClient,
    private configurationService: ConfigurationService,
    private router: Router,
  ) {}

  setToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isLoggedIn() {
    return this.getToken() !== null;
  }

  logIn(loginData: ILoginData): Observable<object> {
    this.userPath = this.configurationService.getValue('requestPathUser', 'notfound');

    return this.http.post(this.userPath, loginData);
  }

  logOut() {
    this.router.navigate(['login']);
  }
}
