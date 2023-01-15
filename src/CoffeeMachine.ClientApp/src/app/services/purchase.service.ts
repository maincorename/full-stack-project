import { ConfigurationService } from './configuration.service';
import { IPurchaseCoffee } from './../models/purchaseCoffee';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPurchase } from '../models/purchase';

@Injectable({ providedIn: 'root' })
export class PurchaseService {
  private purchasePath: string;

  constructor(private http: HttpClient, private configurationService: ConfigurationService) {
    this.purchasePath = this.configurationService.getValue('requestPathPurchase', 'notfound');
  }

  BuyCoffee(id: string, money: number) {
    return this.http.put<IPurchaseCoffee>(this.purchasePath + `/coffee/${id}?money=${money}`, null);
  }

  GetAll() {
    return this.http.get<IPurchase[]>(this.purchasePath);
  }

  Get(id: string) {
    return this.http.get<IPurchase>(this.purchasePath + `/${id}`);
  }

  DeleteAllByCoffeeId(id: string) {
    return this.http.delete(this.purchasePath + `/all/${id}`);
  }

  Delete(id: string) {
    return this.http.delete(this.purchasePath + `/${id}`);
  }
}
