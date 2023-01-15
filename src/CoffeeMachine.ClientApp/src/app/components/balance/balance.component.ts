import { BalanceService } from './../../services/balance.service';

import {
  Component,
  ViewChild,
  ChangeDetectionStrategy,
  ElementRef,
  ChangeDetectorRef,
  OnInit,
} from '@angular/core';

@Component({
  selector: 'app-input-balance',
  templateUrl: './balance.component.html',
  styleUrls: ['./balance.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BalanceComponent implements OnInit {
  @ViewChild('box') inputBox!: ElementRef;
  money = 0;

  constructor(private balanceService: BalanceService, private ref: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.balanceService.getBalance.subscribe((balance) => {
      this.money = balance;
      this.ref.detectChanges();
    });
  }

  onEnter(e: Event, value: number) {
    e.preventDefault();
    if (!isNaN(value)) {
      this.balanceService.setBalance(value);
    }
    this.inputBox.nativeElement.value = '';
  }
}
