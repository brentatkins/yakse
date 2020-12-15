import { Component, OnInit } from '@angular/core';
import { StockPricingService } from '../services/stock-pricing.service';
import { OrderService } from '../services/order.service';
import { StockOrder, StockPrice } from '../models';
import { ModalService } from "../modal/modal.service";

@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html',
  providers: [StockPricingService, OrderService],
  styleUrls: ['./stocks.component.css'],
})
export class StocksComponent implements OnInit {
  stockPrices: StockPrice[];
  selectedStock?: StockPrice;
  orderPlaced?: StockOrder;

  constructor(private pricingService: StockPricingService, private orderService: OrderService, private modalService: ModalService) {
    this.stockPrices = [];
    this.modalService.watch().subscribe(isOpen => !isOpen && this.onModalClose());
  }

  ngOnInit() {
    this.getStockPrices();
  }

  getStockPrices(): void {
    this.pricingService
      .getStockPrices()
      .subscribe((s) => this.updateStockPrices(s));
  }

  buyStock(stock: StockPrice) {
    this.selectedStock = stock;
    this.modalService.open();
  }

  placeOrder(quantity: number) {
    if (this.selectedStock) {
      const { symbol, lastPrice: bidPrice} = this.selectedStock;
      const order = { customerId: "1", symbol, quantity, bidPrice}

      this.orderService.placeOrder(order)
        .subscribe(() => {
          this.selectedStock = undefined;
          this.orderPlaced = order;
        });
    }
  }

  private onModalClose() {
    this.selectedStock = undefined;
    this.orderPlaced = undefined;
  }

  private updateStockPrices(updatedStockPrices: StockPrice[]) {
    updatedStockPrices.forEach(s => {
      const current = this.stockPrices.find(x => x.symbol === s.symbol);
      if (current) {
        current.lastPrice = s.lastPrice;
        current.delta = s.delta;
        current.deltaRatio = s.deltaRatio;
        current.priceDate = s.priceDate;
      } else {
        this.stockPrices = [...this.stockPrices, s];
      }
    })
  }
}
