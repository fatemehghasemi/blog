using Blog.Application.Exceptions;
using Blog.Application.Interfaces;
using Blog.Application.Likes.Commands.LikeArticle;
using Blog.Application.Likes.Commands.UnlikeArticle;
using Blog.Application.Likes.Queries.GetArticleLikesCount;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Blog.Web.Endpoints;

public static class LikesEndpoints
{
    public static IEndpointRouteBuilder MapLikesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/articles")
            .WithTags("Likes");

        group.MapPost("/{articleId:guid}/like",
                async Task<Results<Created<LikeArticleResponse>, BadRequest<string>, Conflict<string>>> (
                    Guid articleId,
                    HttpContext httpContext,
                    LikeArticleCommandHandler commandHandler,
                    ICommandExecutionPipeline commandPipeline,
                    CancellationToken cancellationToken) =>
                {
                    if (!TryGetClientId(httpContext, out var clientId))
                    {
                        return TypedResults.BadRequest("X-Client-Id header is required.");
                    }

                    try
                    {
                        var response = await commandPipeline.ExecuteAsync(
                            ct => commandHandler.HandleAsync(new LikeArticleCommand
                            {
                                ArticleId = articleId,
                                ClientId = clientId
                            }, ct),
                            cancellationToken);

                        return TypedResults.Created($"/api/articles/{articleId}/like", response);
                    }
                    catch (ArgumentException ex)
                    {
                        return TypedResults.BadRequest(ex.Message);
                    }
                    catch (DuplicateLikeException ex)
                    {
                        return TypedResults.Conflict(ex.Message);
                    }
                    catch (InvalidLikeReferenceException ex)
                    {
                        return TypedResults.Conflict(ex.Message);
                    }
                })
            .WithName("LikeArticle")
            .Produces<LikeArticleResponse>(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status409Conflict);

        group.MapDelete("/{articleId:guid}/like",
                async Task<Results<NoContent, BadRequest<string>>> (
                    Guid articleId,
                    HttpContext httpContext,
                    UnlikeArticleCommandHandler commandHandler,
                    ICommandExecutionPipeline commandPipeline,
                    CancellationToken cancellationToken) =>
                {
                    if (!TryGetClientId(httpContext, out var clientId))
                    {
                        return TypedResults.BadRequest("X-Client-Id header is required.");
                    }

                    await commandPipeline.ExecuteAsync(
                        ct => commandHandler.HandleAsync(new UnlikeArticleCommand
                        {
                            ArticleId = articleId,
                            ClientId = clientId
                        }, ct),
                        cancellationToken);

                    return TypedResults.NoContent();
                })
            .WithName("UnlikeArticle")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<string>(StatusCodes.Status400BadRequest);

        group.MapGet("/{articleId:guid}/likes/count",
                async Task<Ok<int>> (
                    Guid articleId,
                    GetArticleLikesCountQueryHandler queryHandler,
                    CancellationToken cancellationToken) =>
                {
                    var count = await queryHandler.HandleAsync(new GetArticleLikesCountQuery
                    {
                        ArticleId = articleId
                    }, cancellationToken);

                    return TypedResults.Ok(count);
                })
            .WithName("GetArticleLikesCount")
            .Produces<int>(StatusCodes.Status200OK);

        return app;
    }

    private static bool TryGetClientId(HttpContext httpContext, out string clientId)
    {
        clientId = string.Empty;

        if (!httpContext.Request.Headers.TryGetValue("X-Client-Id", out var values))
        {
            return false;
        }

        var raw = values.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(raw))
        {
            return false;
        }

        clientId = raw.Trim();
        return true;
    }
}
