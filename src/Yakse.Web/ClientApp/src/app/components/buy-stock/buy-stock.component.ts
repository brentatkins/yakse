import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { StockPrice } from '../../models';

@Component({
  selector: 'app-buy-stock',
  templateUrl: './buy-stock.component.html'
})
export class BuyStockComponent implements OnInit {
  @Input() stock!: StockPrice;
  @Output() placeOrderEvent: EventEmitter<{
    quantity: number;
    bidPrice: number;
  }> = new EventEmitter<{ quantity: number; bidPrice: number }>();

  quantityControl = new FormControl(null, [
    Validators.required,
    Validators.min(1),
    Validators.max(1000000),
  ]);

  bidPriceControl = new FormControl(null, [
    Validators.required,
    Validators.min(0.000001),
    Validators.max(1000000),
  ]);

  customBidPriceControl = new FormControl(false);

  placeOrder() {
    const bidPrice = this.customBidPriceControl.value
      ? this.bidPriceControl.value
      : this.stock.lastPrice;
    this.placeOrderEvent.emit({
      quantity: this.quantityControl.value,
      bidPrice,
    });
  }

  ngOnInit() {
    this.bidPriceControl.setValue(this.stock.lastPrice);
  }
}
