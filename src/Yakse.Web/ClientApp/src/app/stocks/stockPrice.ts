export interface StockPrice {
  symbol: string;
  lastPrice: number;
  delta: number;
  deltaRatio: number;
  priceDate: Date;
}

export interface StockOrder {
  customerId: string;
  symbol: string;
  quantity: number;
  bidPrice: number;
}
