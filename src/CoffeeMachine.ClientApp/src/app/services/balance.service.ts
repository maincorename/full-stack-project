import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BalanceService {
  private balance:BehaviorSubject<number> = new BehaviorSubject<number>(0);

  get getBalance() {
    return this.balance.asObservable();
  }

  setBalance(balance: number) {
    this.balance.next(balance);
  }

  resetBalance() {
    this.balance.next(0);
  }
}
