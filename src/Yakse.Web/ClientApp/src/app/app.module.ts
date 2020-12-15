import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { ModalModule } from "./modal/modal.module";

import { AppComponent } from './app.component';
import { StocksComponent } from './stocks/stocks.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { StockPriceMovementComponent } from './stock-price-movement/stock-price-movement.component';
import { BuyStockComponent } from './buy-stock/buy-stock.component';
import {ReactiveFormsModule} from "@angular/forms";
import { StockPriceStalenessComponent } from './stock-price-staleness/stock-price-staleness.component';

@NgModule({
  declarations: [
    AppComponent,
    StocksComponent,
    BuyStockComponent,
    NavMenuComponent,
    StockPriceMovementComponent,
    BuyStockComponent,
    StockPriceStalenessComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    ModalModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
