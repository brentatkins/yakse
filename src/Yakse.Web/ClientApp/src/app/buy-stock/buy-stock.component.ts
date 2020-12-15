import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import {StockPrice} from "../models";

@Component({
  selector: 'app-buy-stock',
  template: `
    <p class="is-size-3 has-text-weight-bold">{{ stock.symbol}}</p>
    <p>Company: [Company description and name]</p>
    <p>Last trade price: {{ stock.lastPrice | number:'.2' }}</p>
    <p class="is-size-5 is-italic pt-4">To place an order, complete the details below</p>
    <div class="field pt-4">
      <label class="label">Quantity</label>
      <div class="control">
        <input class="input is-info" autofocus [class.is-danger]="quantityControl.invalid && (quantityControl.dirty || quantityControl.touched)" type="number" [formControl]="quantityControl">
      </div>
      <ng-container [ngSwitch]="quantityControl.invalid && (quantityControl.dirty || quantityControl.touched)">
        <p class="help is-danger" *ngSwitchCase="true">Quantity is invalid</p>
        <p class="help is-info" *ngSwitchCase="false">Units to order</p>
      </ng-container>

    </div>
    <div class="field is-grouped">
      <div class="control">
        <button class="button is-info" [disabled]="!quantityControl.valid" (click)="placeOrder()">Place Order</button>
      </div>
    </div>
  `
})
export class BuyStockComponent {
  @Input() stock!: StockPrice;
  @Output() placeOrderEvent: EventEmitter<number> = new EventEmitter<number>();

  quantityControl = new FormControl(null, [
    Validators.required,
    Validators.min(1),
    Validators.max(1000)
  ]);

  placeOrder() {
    this.placeOrderEvent.emit(this.quantityControl.value);
  }
}
