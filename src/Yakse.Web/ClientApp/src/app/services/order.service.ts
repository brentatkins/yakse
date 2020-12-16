import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Order, StockOrder} from '../models';

@Injectable()
export class OrderService {
  private ordersBaseUrl = '/api/order';

  constructor(private http: HttpClient) {
  }

  placeOrder(order: StockOrder) {
    return this.http.post(this.ordersBaseUrl, order);
  }

  getOrderHistory() {
    return this.http.get<Order[]>(this.ordersBaseUrl);
  }
}
