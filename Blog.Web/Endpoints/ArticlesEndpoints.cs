using Blog.Application.DTOs;
using Blog.Application.Exceptions;
using Blog.Application.Features.Articles.Commands.CreateArticle;
using Blog.Application.Features.Articles.Queries.GetArticles;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Blog.Web.Endpoints;

public static class ArticlesEndpoints
{
    public static IEndpointRouteBuilder MapArticlesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/articles")
            .WithTags("Articles");

        group.MapPost("/",
                async Task<Results<Created<CreateArticleResponse>, BadRequest<string>, Conflict<string>>> (
                    CreateArticleRequest request,
                    CreateArticleCommandHandler handler,
                    CancellationToken cancellationToken) =>
                {
                    try
                    {
                        var response = await handler.HandleAsync(new CreateArticleCommand
                        {
                            Title = request.Title,
                            Content = request.Content
                        }, cancellationToken);

                        return TypedResults.Created($"/api/articles/{response.Id}", response);
                    }
                    catch (ArgumentException ex)
                    {
                        return TypedResults.BadRequest(ex.Message);
                    }
                    catch (DuplicateSlugException ex)
                    {
                        return TypedResults.Conflict(ex.Message);
                    }
                })
            .WithName("CreateArticle")
            .Produces<CreateArticleResponse>(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status409Conflict);

        group.MapGet("/",
                async Task<Ok<IReadOnlyList<ArticleDto>>> (
                    GetArticlesQueryHandler handler,
                    CancellationToken cancellationToken) =>
                {
                    var response = await handler.HandleAsync(new GetArticlesQuery(), cancellationToken);
                    return TypedResults.Ok(response);
                })
            .WithName("GetArticles")
            .Produces<IReadOnlyList<ArticleDto>>(StatusCodes.Status200OK);

        return app;
    }
}
