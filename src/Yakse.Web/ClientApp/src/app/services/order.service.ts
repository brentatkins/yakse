import {Injectable, OnDestroy} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Order, StockOrder} from '../models';
import {Observable, Subject, timer} from "rxjs";
import {retry, share, switchMap, takeUntil} from "rxjs/operators";

@Injectable()
export class OrderService implements OnDestroy  {
  private ordersBaseUrl = '/api/order';
  private readonly userBalance$: Observable<number>;
  private stopPolling$ = new Subject();

  constructor(private http: HttpClient) {
    this.userBalance$ = timer(1, 5000).pipe(
      switchMap(() => this.http.get<number>(`${this.ordersBaseUrl}/balance`)),
      retry(),
      share(),
      takeUntil(this.stopPolling$),
    );
  }

  placeOrder(order: StockOrder) {
    return this.http.post(this.ordersBaseUrl, order);
  }

  getOrderHistory() {
    return this.http.get<Order[]>(this.ordersBaseUrl);
  }

  getUserBalance() {
    return this.userBalance$;
  }

  ngOnDestroy(): void {
    this.stopPolling$.next();
  }
}
