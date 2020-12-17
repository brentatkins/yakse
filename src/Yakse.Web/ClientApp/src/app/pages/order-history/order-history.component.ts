import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-order-history',
  template: `
    <app-page title="Order History">
      <app-order-history-table
        [orderHistory$]="this.orderHistory$"
      ></app-order-history-table>
    </app-page>
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
