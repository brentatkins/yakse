import { Component, OnInit } from '@angular/core';
import { OrderService } from '../services/order.service';
import { Observable } from 'rxjs';
import {CustomerBalance} from "../models";

@Component({
  selector: 'app-customer-balance',
  template: `
    <span>
      Cash Balance: € {{ (customerBalance$ | async)?.cashBalance | number:"1.2-2" }},
      Portfolio Balance: € {{ (customerBalance$ | async)?.portfolioBalance | number:"1.2-2" }}
    </span>
  `,
  providers: [OrderService],
})
export class CustomerBalanceComponent implements OnInit {
  customerBalance$!: Observable<CustomerBalance>;

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.customerBalance$ = this.orderService.getCustomerBalance();
  }
}
