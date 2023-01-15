import { CoffeeService } from './../../../services/coffee.service';
import { ICoffee } from 'src/app/models/coffee';
import { Injectable } from '@angular/core';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { catchError, EMPTY, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CoffeeResolver implements Resolve<ICoffee> {
  constructor(private coffeeService: CoffeeService, private router: Router) {}

  resolve(route: ActivatedRouteSnapshot): Observable<ICoffee> {
    return this.coffeeService.Get(route.params?.['id']).pipe(
      catchError(() => {
        this.router.navigate(['admin/coffees']);
        return EMPTY;
      }),
    );
  }
}
