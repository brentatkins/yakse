import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockPriceMovementComponent } from './stock-price-movement.component';

describe('StockPriceMovementComponent', () => {
  let component: StockPriceMovementComponent;
  let fixture: ComponentFixture<StockPriceMovementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockPriceMovementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockPriceMovementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
