import {
  Component,
  Input,
  OnChanges,
  OnInit,
  OnDestroy,
  SimpleChanges,
} from '@angular/core';
import { merge, Observable, Subject, Subscription, timer } from 'rxjs';
import {
  distinctUntilChanged,
  filter,
  map,
  mapTo,
  startWith,
  switchMap,
  tap,
} from 'rxjs/operators';
import * as moment from 'moment';

@Component({
  selector: 'app-stock-price-staleness',
  template: ` <span *ngIf="isStale" class="is-flex is-align-items-center">
      <span class="icon has-text-warning">
        <i class="fas fa-exclamation-triangle"></i>
      </span>
      <span class="is-size-7"
        >Last update {{ priceAge | number: '.0-0' }}s ago</span
      >
    </span>
    <span *ngIf="!isStale" class="icon">
      <i class="fas fa-exclamation-triangle is-invisible"></i
    ></span>`,
})
export class StockPriceStalenessComponent
  implements OnInit, OnChanges, OnDestroy {
  @Input() priceDate!: Date;
  isStale: boolean = false;
  priceAge?: number;

  private stalenessThreshold = 10000;

  private priceDateChanged$ = new Subject();
  private timer$: Observable<any>;
  private subscription!: Subscription;

  constructor() {
    this.timer$ = this.priceDateChanged$.pipe(
      startWith(0),
      switchMap(() => timer(0, this.stalenessThreshold)),
      map((v) => v > 0),
      distinctUntilChanged()
    );
  }

  ngOnInit() {
    this.subscription = this.timer$.subscribe((isStale) => {
      this.priceAge = moment
        .duration(moment().diff(this.priceDate))
        .asSeconds();
      this.isStale = isStale;
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.priceDate.currentValue !== changes.priceDate.previousValue) {
      this.priceDateChanged$.next();
    }
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
