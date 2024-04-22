using Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<DataSourceContext>(options => SetupApplicationDbContextOptions(options, configuration));
            
            return services.AddSingleton<IDataSource, DataSourceInMemory>();
        }

        private static void SetupApplicationDbContextOptions(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("FreightExchangeDbConnection"));

            #if DEBUG
            optionsBuilder.EnableDetailedErrors();
            #endif
        }
    }
}
