import {Component, Input, Output, EventEmitter, OnInit} from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import {StockPrice} from "../models";

@Component({
  selector: 'app-buy-stock',
  template: `
    <p class="is-size-3 has-text-weight-bold">{{ stock.symbol}}</p>
    <p>Company: [Company description and name]</p>
    <p>Last trade price: <span class="is-size-5 has-text-weight-bold">{{ stock.lastPrice | number:'.3-3' }}</span></p>
    <p class="is-size-5 is-italic pt-4">To place an order, complete the details below</p>
    <div class="field pt-4">
      <label class="label">Quantity</label>
      <div class="control">
        <input class="input is-info" [class.is-danger]="quantityControl.invalid && (quantityControl.dirty || quantityControl.touched)" type="number" [formControl]="quantityControl">
      </div>
      <ng-container [ngSwitch]="quantityControl.invalid && (quantityControl.dirty || quantityControl.touched)">
        <p class="help is-danger" *ngSwitchCase="true">Quantity is invalid</p>
        <p class="help is-info" *ngSwitchCase="false">Units to order</p>
      </ng-container>
    </div>
    <label class="checkbox">
      <input type="checkbox" [formControl]="customBidPriceControl">
      Don't like the current price? Set a custom one
    </label>
    <div *ngIf="customBidPriceControl.value" class="field pt-4">
      <label class="label">Bid Price</label>
      <div class="control">
        <input class="input is-info" autofocus [class.is-danger]="bidPriceControl.invalid && (bidPriceControl.dirty || bidPriceControl.touched)" type="number" [formControl]="bidPriceControl">
      </div>
    </div>
    <div class="field is-grouped pt-4">
      <div class="control">
        <button class="button is-info" [disabled]="!quantityControl.valid" (click)="placeOrder()">Place Order</button>
      </div>
    </div>
  `
})
export class BuyStockComponent implements OnInit {
  @Input() stock!: StockPrice;
  @Output() placeOrderEvent: EventEmitter<{quantity: number, bidPrice: number}> = new EventEmitter<{quantity: number, bidPrice: number}>();

  quantityControl = new FormControl(null, [
    Validators.required,
    Validators.min(1),
    Validators.max(1000000)
  ]);

  bidPriceControl = new FormControl(null, [
    Validators.required,
    Validators.min(0.000001),
    Validators.max(1000000)
  ]);

  customBidPriceControl = new FormControl(false);

  placeOrder() {
    const bidPrice = this.customBidPriceControl.value ? this.bidPriceControl.value : this.stock.lastPrice;
    this.placeOrderEvent.emit({quantity: this.quantityControl.value, bidPrice});
  }

  ngOnInit() {
    this.bidPriceControl.setValue(this.stock.lastPrice);
  }
}
