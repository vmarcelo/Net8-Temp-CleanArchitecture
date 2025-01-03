using FlatFinder.Application.Abstractions.Behaviors;
using FlatFinder.Domain.Reservations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FlatFinder.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                //explora el ensamblado paraq registrar automaticamente todas las clases que implementen las interfaces de mediatr.
                options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                options.AddOpenBehavior(typeof(LoggingBehavior<,>));
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            services.AddTransient<PricingService>();
            return services;
        }
    }
}
