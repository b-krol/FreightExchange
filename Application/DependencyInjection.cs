using Application.Users;
using Application.CartageErrands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CartageOffers;
using AutoMapper;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICartageErrandService, CartageErrandService>();
            services.AddScoped<ICartageOfferService, CartageOfferService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.Configure<FinalizerConfiguration>(x => { configuration.GetSection(""); });
            services.AddOptions<FinalizerConfiguration>()
                .Bind(configuration.GetSection("CartageErrandsFinalizer"));
            services.AddHostedService<CartageErrandsFinalizer>();
            return services;
        }
    }
}
