import { Component, OnInit } from '@angular/core';
import { OrderService } from '../services/order.service';
import { Order } from '../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-order-history',
  template: `
    <div class="is-flex is-align-items-center is-flex-direction-column">
      <div class="is-size-3">Order History</div>
      <div>
        <app-order-history-table
          [orderHistory$]="this.orderHistory$"
        ></app-order-history-table>
      </div>
    </div>
  `,
  providers: [OrderService],
})
export class OrderHistoryComponent implements OnInit {
  orderHistory$!: Observable<Order[]>;

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    this.orderHistory$ = this.orderService.getOrderHistory();
  }
}
