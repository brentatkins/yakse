import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StocksComponent } from './pages/stocks/stocks.component';
import { OrderHistoryComponent } from './pages/order-history/order-history.component';

const routes: Routes = [
  { path: '', component: StocksComponent, pathMatch: 'full' },
  { path: 'orders', component: OrderHistoryComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
