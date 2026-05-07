using Blog.Application.Execution;
using Blog.Application.Features.Articles.Commands.CreateArticle;
using Blog.Application.Features.Articles.Queries.GetArticles;
using Blog.Application.Features.Comments.CreateComment;
using Blog.Application.Features.Comments.GetCommentsByArticle;
using Blog.Application.Features.Likes.GetArticleLikesCount;
using Blog.Application.Features.Likes.LikeArticle;
using Blog.Application.Features.Likes.UnlikeArticle;
using Blog.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICommandExecutionPipeline, CommandExecutionPipeline>();

        services.AddScoped<CreateArticleCommandHandler>();
        services.AddScoped<GetArticlesQueryHandler>();
        services.AddScoped<CreateCommentHandler>();
        services.AddScoped<GetCommentsHandler>();
        services.AddScoped<LikeArticleHandler>();
        services.AddScoped<UnlikeArticleHandler>();
        services.AddScoped<GetArticleLikesCountHandler>();

        return services;
    }
}
