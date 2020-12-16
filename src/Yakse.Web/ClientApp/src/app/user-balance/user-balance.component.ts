import { Component, OnInit } from '@angular/core';
import { OrderService } from '../services/order.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-user-balance',
  template:
    '<span>My Balance: € {{ userBalance | async | number:"1.0-2" }}</span>',
  providers: [OrderService],
})
export class UserBalanceComponent implements OnInit {
  userBalance!: Observable<number>;

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.userBalance = this.orderService.getUserBalance();
  }
}
