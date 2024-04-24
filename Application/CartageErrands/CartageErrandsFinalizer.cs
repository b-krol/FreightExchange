using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CartageErrands
{
    internal class CartageErrandsFinalizer : BackgroundService
    {
        private readonly IServiceProvider ServiceProvider;
        private readonly IOptions<FinalizerConfiguration> Options;
        private readonly ILogger<CartageErrandsFinalizer> Logger;

        public CartageErrandsFinalizer(IServiceProvider service, IOptions<FinalizerConfiguration> options, ILogger<CartageErrandsFinalizer> logger)
        {
            ServiceProvider = service;
            Options = options;
            Logger = logger;
            Logger.LogInformation("Period = {Period}", options.Value.ExecutionPeriod);
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {                        
            while (!stoppingToken.IsCancellationRequested)
            {
                IServiceScope scope = ServiceProvider.CreateScope();
                ICartageErrandService cartageErrandService = scope.ServiceProvider.GetRequiredService<ICartageErrandService>();

                await cartageErrandService.FinishErrandsExceedingEndTime();
                await Task.Delay(Options.Value.ExecutionPeriod, stoppingToken);
            }
        }
    }
}
