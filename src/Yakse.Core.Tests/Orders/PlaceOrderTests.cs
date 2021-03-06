using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Yakse.Core.Orders;
using Yakse.Core.Orders.Commands;
using Yakse.Core.Orders.Queries;
using Yakse.Core.Pricing;
using Yakse.Core.Pricing.Commands;
using Yakse.Core.Pricing.Events;

namespace Yakse.Core.Tests.Orders
{
    public class PlaceOrderTests : BaseTest, IAsyncLifetime
    {
        private string _stockSymbol = string.Empty;

        [Fact]
        public async Task PlaceOrder_ShouldBeInOrderHistory()
        {
            // arrange
            var customerId = "customer id 1";
            
            var symbol = _stockSymbol;
            var quantity = 10;
            var bidPrice = 123.32424m;
            var placeOrderCommand = new PlaceOrder(customerId, symbol, quantity, bidPrice);
            
            // act
            await Mediator.Send(placeOrderCommand);
            
            // assert
            var orderHistory = await Mediator.Send(new GetOrderHistory(customerId));

            orderHistory.Should().ContainSingle(x => x.Symbol == symbol);
            var order = orderHistory.Single(x => x.Symbol == symbol);
            order.Quantity.Should().Be(quantity);
            order.BidPrice.Should().Be(bidPrice);
            order.CustomerId.Should().Be(customerId);
        }
        
        [Fact]
        public async Task PlaceOrder_OrderShouldBePending()
        {
            // arrange
            var customerId = "customer id 1";
            
            var symbol = _stockSymbol;
            var quantity = 10;
            var bidPrice = 123.32424m;
            var placeOrderCommand = new PlaceOrder(customerId, symbol, quantity, bidPrice);
            
            // act
            await Mediator.Send(placeOrderCommand);
            
            // assert
            var order = (await Repository.Find<Order>(o => o.CustomerId == customerId)).Single();
            order.Status.Should().Be(Order.OrderStatus.Pending);
        }
        
        [Fact]
        public async Task ExecuteOrder_OrderIsPendingAPriceIsUpdatedToBelow_OrderShouldBeExecuted()
        {
            // arrange
            var customerId = "customer id 1";
            
            var symbol = _stockSymbol;
            var quantity = 10;
            var bidPrice = 123.32424m;
            var placeOrderCommand = new PlaceOrder(customerId, symbol, quantity, bidPrice);
            
            // act
            await Mediator.Send(placeOrderCommand);
            await Mediator.Publish(new StockPriceUpdatedEvent(symbol, bidPrice - 1));
            
            // assert
            var order = (await Repository.Find<Order>(o => o.CustomerId == customerId)).Single();
            order.Status.Should().Be(Order.OrderStatus.Executed);
            order.TradeDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        public async Task InitializeAsync()
        {
            await Mediator.Send(new LoadStocks(1));
            _stockSymbol = (await Repository.All<Stock>()).First().Symbol;
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}