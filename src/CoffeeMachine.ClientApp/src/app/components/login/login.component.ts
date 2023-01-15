import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      login: new FormControl('', [Validators.required]),
      password: new FormControl('', [
        Validators.required,
        Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$/),
      ]),
    });

    if (this.authService.isLoggedIn()) {
      this.router.navigate(['admin']);
    }
  }

  submitLogin() {
    this.authService.logIn(this.loginForm.value).subscribe({
      next: (data: any) => {
        console.log(data['accessToken']);
        this.authService.setToken(data['accessToken']), this.router.navigate(['admin']);
      },
      error: (err) => alert(err.message),
    });
  }
}
