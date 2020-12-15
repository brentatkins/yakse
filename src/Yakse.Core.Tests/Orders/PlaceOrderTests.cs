using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Yakse.Core.Orders.Commands;
using Yakse.Core.Orders.Queries;

namespace Yakse.Core.Tests.Orders
{
    public class PlaceOrderTests : BaseTest
    {
        [Fact]
        public async Task PlaceOrder_ReducesBalanceByCorrectAmount()
        {
            // arrange
            var customerId = "customer id 1";
            var balanceBefore = await Mediator.Send(new GetCustomerBalance(customerId));
            
            var symbol = "aaa";
            var quantity = 10;
            var bidPrice = 123.32424m;
            var placeOrderCommand = new PlaceOrderCommand(customerId, symbol, quantity, bidPrice);
            
            // act
            await Mediator.Send(placeOrderCommand);
            
            // assert
            var balanceAfter = await Mediator.Send(new GetCustomerBalance(customerId));
            var balanceReduction = quantity * bidPrice;

            balanceAfter.Should().Be(balanceBefore - balanceReduction);
        } 
    }
}