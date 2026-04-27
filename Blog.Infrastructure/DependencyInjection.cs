using Blog.Application.Abstractions.Persistence;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.Persistence.Repositories;
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
        services.AddScoped<IArticleRepository, ArticleRepository>();

        return services;
    }
}
