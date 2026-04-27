using Blog.Application.Features.Articles.Commands.CreateArticle;
using Blog.Application.Features.Articles.Queries.GetArticles;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateArticleCommandHandler>();
        services.AddScoped<GetArticlesQueryHandler>();

        return services;
    }
}
