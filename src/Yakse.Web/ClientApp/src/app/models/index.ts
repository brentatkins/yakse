export interface StockPrice {
  symbol: string;
  lastPrice: number;
  delta: number;
  deltaRatio: number;
  priceDate: Date;
}

export interface StockOrder {
  symbol: string;
  quantity: number;
  bidPrice: number;
}

export interface Order {
  symbol: string;
  quantity: number;
  bidPrice: number;
  orderDate: Date;
  status: string;
  tradePrice?: number;
  tradeDate?: Date;
  total: number;
}
