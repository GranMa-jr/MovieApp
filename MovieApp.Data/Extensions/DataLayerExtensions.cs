using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Data.Context;
using MovieApp.Data.Repositories.Abstractions;
using MovieApp.Data.Repositories.Concretes;
using MovieApp.Data.UnitOfWorks;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace MovieApp.Data.Extensions
{
    public static class DataLayerExtensions
    {
        public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseMySql(config.GetConnectionString("DefaultConnection"), 
                new MySqlServerVersion(new Version(8, 0, 21))));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
