using System;
using MessagingService.Data;
using MessagingService.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingService.Service
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IErrorService, ErrorService>();
        }

        public static void ConfigureDatabase(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable(Constants.ConnectionStringEnvironmentVariableName);
            var databaseName = Environment.GetEnvironmentVariable(Constants.DatabaseNameEnvironmentVariableName);

            if (string.IsNullOrEmpty(connectionString)) throw new Exception("Connection string not set!");
            if (string.IsNullOrEmpty(databaseName)) throw new Exception("Database name not set!");

            services.AddScoped<IMongoDbContext>(x => new MessagingServiceContext(connectionString, databaseName));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}