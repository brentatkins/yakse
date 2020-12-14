import { Component, OnInit } from '@angular/core';
import { StocksService } from './stocks.service';
import { StockPrice } from './stockPrice';

@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html',
  providers: [StocksService],
  styleUrls: ['./stocks.component.css'],
})
export class StocksComponent implements OnInit {
  stockPrices: StockPrice[];

  constructor(private stocksService: StocksService) {
    this.stockPrices = []
  }

  ngOnInit() {
    this.getStockPrices();
  }

  getStockPrices(): void {
    this.stocksService
      .getStockPrices()
      .subscribe((stockPrices) => (this.stockPrices = stockPrices));
  }
}
