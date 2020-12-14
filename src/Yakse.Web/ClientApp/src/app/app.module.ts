import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StocksComponent } from './stocks/stocks.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { StockPriceMovementComponent } from './stock-price-movement/stock-price-movement.component';

@NgModule({
  declarations: [
    AppComponent,
    StocksComponent,
    NavMenuComponent,
    StockPriceMovementComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
