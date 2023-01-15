import { Observable } from 'rxjs';
import { PurchaseService } from './../../services/purchase.service';
import { IPurchaseCoffee } from './../../models/purchaseCoffee';
import {
  ChangeDetectionStrategy,
  Component,
  OnInit,
  ChangeDetectorRef,
  ViewChild,
  TemplateRef,
} from '@angular/core';
import { ICoffee } from '../../models/coffee';
import { CoffeeService } from '../../services/coffee.service';
import { BalanceService } from 'src/app/services/balance.service';

@Component({
  selector: 'app-coffee-list',
  templateUrl: './coffee.component.html',
  styleUrls: ['./coffee.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CoffeeComponent implements OnInit {
  @ViewChild('errorTemplate', { static: false }) errorTemplate!: TemplateRef<any> | null;
  @ViewChild('buyCoffeeTemplate', { static: false }) buyCoffeeTemplate!: TemplateRef<any> | null;
  coffees!: Observable<ICoffee[]>;
  purchase: IPurchaseCoffee = {
    purchasedCoffee: { id: '', name: '', price: 0 },
    changeBanknotes: [],
  };
  money = 0;
  isPurchased = false;
  errorMessage = '';

  constructor(
    private coffeeService: CoffeeService,
    private balanceService: BalanceService,
    private purchaseService: PurchaseService,
    private ref: ChangeDetectorRef,
  ) {}

  ngOnInit() {
    this.coffees = this.coffeeService.GetAll();
    this.balanceService.getBalance.subscribe((balance) => (this.money = balance));
  }

  onSelect(coffee: ICoffee): IPurchaseCoffee | void {
    this.purchaseService.BuyCoffee(coffee.id, this.money).subscribe(
      (Response: IPurchaseCoffee) => {
        this.isPurchased = true;
        this.purchase.changeBanknotes = Response.changeBanknotes;
        this.purchase.purchasedCoffee = Response.purchasedCoffee;
        this.balanceService.resetBalance();
        this.ref.detectChanges();
      },
      (error) => {
        this.isPurchased = false;
        this.errorMessage = error['error'];
        this.ref.detectChanges();
      },
    );
  }

  loadTemplate() {
    if (this.isPurchased) {
      return this.buyCoffeeTemplate;
    } else {
      return this.errorTemplate;
    }
  }
}
