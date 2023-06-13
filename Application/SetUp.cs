using Application.Entities;
using Application.Managers;
using Application.Mappers;
using Application.Repositories;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    static class SetUp
    {
        private static IMapper SetUpMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EnumsProfile>();
            });

            return configuration.CreateMapper();
        }
        public static IServiceProvider SetUpServiceCollection(ServerConfig config)
        {
            var services = new ServiceCollection();
            var mapper = SetUpMapper();
            services.AddSingleton(mapper);
            services.AddSingleton(config);
            services.AddSingleton(new CSVEnumConverter<Daytime>(typeof(Daytime), mapper));
            services.AddSingleton(new CSVEnumConverter<DishType>(typeof(DishType), mapper));

            services.AddScoped<IInputManager, InputManager>();
            services.AddScoped<IOutputManager, OutputManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IMenuRepository, MenuRepository>();

            return services.BuildServiceProvider();
        }
    }
}
