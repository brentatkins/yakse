import {Component, Input, OnChanges, OnInit, OnDestroy, SimpleChanges} from '@angular/core';
import {merge, Observable, Subject, Subscription, timer} from "rxjs";
import {filter, map, mapTo, startWith, switchMap, tap} from "rxjs/operators";

@Component({
  selector: 'app-stock-price-staleness',
  template: `
      <span *ngIf="isStale" class="icon has-text-warning">
        <i class="fas fa-exclamation-triangle"></i>
      </span>
  <span *ngIf="!isStale" class="icon"></span>`
})
export class StockPriceStalenessComponent implements OnInit, OnChanges, OnDestroy {
  @Input() priceDate!: Date;
  isStale: boolean = false;

  private stalenessThreshold = 6000;

  private priceDateChanged$ = new Subject();
  private timer$: Observable<any>;
  private subscription!: Subscription;

  constructor() {
    this.timer$ = this.priceDateChanged$.pipe(
      startWith(0),
      switchMap(() => timer(0, this.stalenessThreshold)),
      map(v => v > 0)
    );
  }

  ngOnInit(){
    this.subscription = this.timer$.subscribe(isStale => {
      this.isStale = isStale
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.priceDate.currentValue !== changes.priceDate.previousValue) {
      this.priceDateChanged$.next();
    }
  }

  ngOnDestroy(){
    if (this.subscription){
      this.subscription.unsubscribe();
    }
  }
}
