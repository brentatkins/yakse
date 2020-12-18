import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { ModalModule } from './modal/modal.module';

import { AppComponent } from './app.component';
import { StocksComponent } from './pages/stocks/stocks.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { StockPriceMovementComponent } from './components/stock-price-movement/stock-price-movement.component';
import { BuyStockComponent } from './components/buy-stock/buy-stock.component';
import { ReactiveFormsModule } from '@angular/forms';
import { StockPriceStalenessComponent } from './components/stock-price-staleness/stock-price-staleness.component';
import { StockPriceTableComponent } from './components/stock-price-table/stock-price-table.component';
import { OrderHistoryComponent } from './pages/order-history/order-history.component';
import { OrderHistoryTableComponent } from './components/order-history-table/order-history-table.component';
import { CustomerBalanceComponent } from './components/customer-balance/customer-balance.component';
import { PageComponent } from './components/page/page.component';
import { MovementHintComponent } from './components/movement-hint/movement-hint.component';

@NgModule({
  declarations: [
    AppComponent,
    StocksComponent,
    BuyStockComponent,
    NavMenuComponent,
    StockPriceMovementComponent,
    BuyStockComponent,
    StockPriceStalenessComponent,
    StockPriceTableComponent,
    OrderHistoryComponent,
    OrderHistoryTableComponent,
    CustomerBalanceComponent,
    PageComponent,
    MovementHintComponent,
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    ModalModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
