import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-stock-price-movement',
  template: `
    <span
      [class]="{
        'has-text-success': priceChange > 0,
        'has-text-danger': priceChange < 0
      }"
    >
      <ng-content></ng-content>
    </span>
  `,
  styles: [],
})
export class StockPriceMovementComponent {
  @Input() priceChange!: number;
}
