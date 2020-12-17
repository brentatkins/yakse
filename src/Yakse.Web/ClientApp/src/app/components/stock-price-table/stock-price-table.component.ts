import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { StockPrice } from '../../models';

@Component({
  selector: 'app-stock-price-table',
  templateUrl: './stock-price-table.component.html',
  styleUrls: ['./stock-price-table.component.css'],
})
export class StockPriceTableComponent implements OnInit {
  @Input() stockPrices: StockPrice[];
  @Output()
  buyStockEvent: EventEmitter<StockPrice> = new EventEmitter<StockPrice>();

  constructor() {
    this.stockPrices = [];
  }

  buyStock(stockPrice: StockPrice) {
    this.buyStockEvent.emit(stockPrice);
  }

  ngOnInit(): void {}
}
