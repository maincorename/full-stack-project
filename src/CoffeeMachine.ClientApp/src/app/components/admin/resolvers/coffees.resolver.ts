import { CoffeeService } from './../../../services/coffee.service';
import { ICoffee } from 'src/app/models/coffee';
import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CoffeesResolver implements Resolve<ICoffee[]> {
  constructor(private coffeeService: CoffeeService) {}

  resolve(): Observable<ICoffee[]> {
    return this.coffeeService.GetAll();
  }
}
