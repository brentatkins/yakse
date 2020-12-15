import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockPriceStalenessComponent } from './stock-price-staleness.component';

describe('StockPriceStalenessComponent', () => {
  let component: StockPriceStalenessComponent;
  let fixture: ComponentFixture<StockPriceStalenessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockPriceStalenessComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockPriceStalenessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
