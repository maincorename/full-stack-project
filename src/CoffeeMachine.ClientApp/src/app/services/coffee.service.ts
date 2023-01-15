import { Observable } from 'rxjs';
import { ICreatedCoffee } from './../models/createdCoffee';
import { ConfigurationService } from './configuration.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICoffee } from 'src/app/models/coffee';

@Injectable({
  providedIn: 'root',
})
export class CoffeeService {
  private coffeePath: string;

  constructor(private http: HttpClient, private configurationService: ConfigurationService) {
    this.coffeePath = this.configurationService.getValue('requestPathCoffee', 'notfound');
  }

  GetAll() {
    return this.http.get<ICoffee[]>(this.coffeePath);
  }

  Get(id: string) {
    return this.http.get<ICoffee>(this.coffeePath + `/${id}`);
  }

  Update(coffee: ICoffee) {
    return this.http.put(this.coffeePath, coffee);
  }

  Delete(id: string) {
    return this.http.delete(this.coffeePath + `/${id}`);
  }

  Create(coffee: ICreatedCoffee) {
    const body = { name: coffee.name, price: coffee.price };

    return this.http.post<Observable<any>>(this.coffeePath, body);
  }
}
