import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { PurchaseService } from './../../../../services/purchase.service';
import { Component, OnInit } from '@angular/core';
import { IPurchase } from 'src/app/models/purchase';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
})
export class PurchasesComponent implements OnInit {
  purchases!: Observable<IPurchase[]>;

  constructor(private purchaseService: PurchaseService, private router: Router) {}

  ngOnInit(): void {
    this.purchases = this.purchaseService.GetAll();
  }
}
