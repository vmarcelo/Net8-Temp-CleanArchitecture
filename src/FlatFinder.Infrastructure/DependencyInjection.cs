using Dapper;
using FlatFinder.Application.Abstractions.Communication;
using FlatFinder.Application.Abstractions.Data;
using FlatFinder.Application.Abstractions.Time;
using FlatFinder.Domain.Abstractions;
using FlatFinder.Domain.Flats;
using FlatFinder.Domain.Reservations;
using FlatFinder.Domain.Users;
using FlatFinder.Infrastructure.Flats;
using FlatFinder.Infrastructure.Implementations.Communication;
using FlatFinder.Infrastructure.Implementations.Data;
using FlatFinder.Infrastructure.Reservations;
using FlatFinder.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FlatFinder.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            var connectionString = configuration.GetConnectionString("defaultConnection") ??
                    throw new ArgumentNullException(nameof(configuration));

            services.AddTransient<ITimeProvider, Implementations.Time.TimeProvider>();
            services.AddTransient<IEmailService, EmailService>();

            //Configuring the context
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFlatRepository, FlatRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();

            services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<ISqlConnectionFactory>(_ =>
                new SqlConnectionFactory(connectionString));
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

            //ejecutar las migraciones de forma automática
            services.AddHostedService<MigrationHostedService>();

            return services;
        }
    }

    public class MigrationHostedService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;

        public MigrationHostedService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
