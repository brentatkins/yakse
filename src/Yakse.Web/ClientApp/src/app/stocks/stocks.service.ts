import { Injectable, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, timer , Subject } from 'rxjs';
import { share, switchMap, takeUntil, retry } from 'rxjs/operators';
import { StockPrice } from './stockPrice';

@Injectable()
export class StocksService implements OnDestroy {
  private stockPriceUrl = '/api/stock';
  private pollInterval = 5000;
  private stockPrices$: Observable<StockPrice[]>;
  private stopPolling$ = new Subject();

  constructor(private http: HttpClient) {
    this.stockPrices$ = timer(1, this.pollInterval).pipe(
      switchMap(() => this.http.get<StockPrice[]>(this.stockPriceUrl)),
      retry(),
      share(),
      takeUntil(this.stopPolling$),
    );
    
  }
  ngOnDestroy(): void {
    this.stopPolling$.next();
  }

  getStockPrices() {
    return this.stockPrices$;
  }
}
