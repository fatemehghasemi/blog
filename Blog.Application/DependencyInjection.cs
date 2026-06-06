using Blog.Application.Execution;
using Blog.Application.Articles.Commands.CreateArticle;
using Blog.Application.Articles.Queries.GetArticleById;
using Blog.Application.Articles.Queries.GetArticlesList;
using Blog.Application.Comments.Commands.AddComment;
using Blog.Application.Comments.Queries.GetCommentsByArticle;
using Blog.Application.Interfaces;
using Blog.Application.Likes.Commands.LikeArticle;
using Blog.Application.Likes.Commands.UnlikeArticle;
using Blog.Application.Likes.Queries.GetArticleLikesCount;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddScoped<ICommandExecutionPipeline, CommandExecutionPipeline>();

        services.AddScoped<CreateArticleCommandHandler>();
        services.AddScoped<GetArticlesListQueryHandler>();
        services.AddScoped<GetArticleByIdQueryHandler>();
        services.AddScoped<AddCommentCommandHandler>();
        services.AddScoped<GetCommentsByArticleQueryHandler>();
        services.AddScoped<LikeArticleCommandHandler>();
        services.AddScoped<UnlikeArticleCommandHandler>();
        services.AddScoped<GetArticleLikesCountQueryHandler>();

        return services;
    }
}
