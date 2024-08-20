using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Reflection;
using MovieApp.Service.Services.Abstractions;
using MovieApp.Service.Services.Concrete;
using MovieApp.Service.Services.Abstract;

namespace MovieApp.Service.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(cfg => cfg.AddMaps(assembly));

            services.AddControllers()
                .AddFluentValidation(opt =>
                {
                    opt.RegisterValidatorsFromAssembly(assembly);
                    opt.DisableDataAnnotationsValidation = true;
                    opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
                });

            return services;
        }
    }
}