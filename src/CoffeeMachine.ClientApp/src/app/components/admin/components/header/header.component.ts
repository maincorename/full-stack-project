import { ResolveEnd, ResolveStart, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { filter, map, merge, Observable } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  private showLoader!: Observable<boolean>;
  private hideLoader!: Observable<boolean>;
  isLoading!: Observable<boolean>;

  constructor(private authService: AuthService, private router: Router) {}

  logOut() {
    this.authService.logOut();
  }

  ngOnInit(): void {
    this.hideLoader = this.router.events.pipe(
      filter((event) => event instanceof ResolveEnd),
      map(() => false),
    );
    this.showLoader = this.router.events.pipe(
      filter((event) => event instanceof ResolveStart),
      map(() => true),
    );

    this.isLoading = merge(this.hideLoader, this.showLoader);
  }
}
