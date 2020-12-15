using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Yakse.Core.Pricing.Commands;
using Yakse.Infrastructure;

namespace Yakse.Core.Tests
{
    public abstract class BaseTest
    {
        protected IMediator Mediator { get; }

        protected BaseTest()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddMediatR(typeof(LoadStocksCommand));
            serviceCollection.AddScoped<IRepository, InMemoryRepository>();
            
            var provider = serviceCollection.BuildServiceProvider();

            Mediator = provider.GetRequiredService<IMediator>();
        }
    }
}