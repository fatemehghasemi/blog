using Blog.Application.Features.Articles.Commands.CreateArticle;
using Blog.Application.Features.Articles.Queries.GetArticles;
using Blog.Application.Features.Comments.CreateComment;
using Blog.Application.Features.Comments.GetCommentsByArticle;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateArticleCommandHandler>();
        services.AddScoped<GetArticlesQueryHandler>();
        services.AddScoped<CreateCommentHandler>();
        services.AddScoped<GetCommentsHandler>();

        return services;
    }
}
