import { Component, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { Order } from '../../models';

@Component({
  selector: 'app-order-history-table',
  templateUrl: './order-history-table.component.html',
  styleUrls: ['./order-history-table.component.css'],
})
export class OrderHistoryTableComponent {
  @Input() orderHistory$!: Observable<Order[]>;
}
