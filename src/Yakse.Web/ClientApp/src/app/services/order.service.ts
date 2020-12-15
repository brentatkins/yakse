import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StockOrder } from '../models';

@Injectable()
export class OrderService {
  private placeOrderUrl = '/api/order';

  constructor(private http: HttpClient) {
  }

  placeOrder(order: StockOrder) {
    return this.http.post(this.placeOrderUrl, order);
  }
}
