using Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")
            ?? configuration.GetConnectionString("blog")
            ?? "Server=localhost\\SQLEXPRESS;Database=blogdb;Trusted_Connection=True;TrustServerCertificate=True;";

        services.AddDbContext<BlogDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}
