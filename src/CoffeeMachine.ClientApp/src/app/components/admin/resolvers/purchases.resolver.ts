import { IPurchase } from 'src/app/models/purchase';
import { PurchaseService } from './../../../services/purchase.service';
import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PurchasesResolver implements Resolve<IPurchase[]> {
  constructor(private purchaseService: PurchaseService) {}

  resolve(): Observable<IPurchase[]> {
    return this.purchaseService.GetAll();
  } 
}
