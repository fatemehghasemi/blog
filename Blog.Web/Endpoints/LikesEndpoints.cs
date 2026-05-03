using Blog.Application.DTOs;
using Blog.Application.Exceptions;
using Blog.Application.Features.Likes.GetArticleLikesCount;
using Blog.Application.Features.Likes.LikeArticle;
using Blog.Application.Features.Likes.UnlikeArticle;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Blog.Web.Endpoints;

public static class LikesEndpoints
{
    public static IEndpointRouteBuilder MapLikesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/articles")
            .WithTags("Likes");

        group.MapPost("/{articleId:guid}/like",
                async Task<Results<Created<LikeResponse>, BadRequest<string>, Conflict<string>>> (
                    Guid articleId,
                    HttpContext httpContext,
                    LikeArticleHandler handler,
                    CancellationToken cancellationToken) =>
                {
                    if (!TryGetClientId(httpContext, out var clientId))
                    {
                        return TypedResults.BadRequest("X-Client-Id header is required.");
                    }

                    try
                    {
                        var response = await handler.HandleAsync(new LikeArticleCommand
                        {
                            ArticleId = articleId,
                            ClientId = clientId
                        }, cancellationToken);

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
            .Produces<LikeResponse>(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status409Conflict);

        group.MapDelete("/{articleId:guid}/like",
                async Task<Results<NoContent, BadRequest<string>>> (
                    Guid articleId,
                    HttpContext httpContext,
                    UnlikeArticleHandler handler,
                    CancellationToken cancellationToken) =>
                {
                    if (!TryGetClientId(httpContext, out var clientId))
                    {
                        return TypedResults.BadRequest("X-Client-Id header is required.");
                    }

                    await handler.HandleAsync(new UnlikeArticleCommand
                    {
                        ArticleId = articleId,
                        ClientId = clientId
                    }, cancellationToken);

                    return TypedResults.NoContent();
                })
            .WithName("UnlikeArticle")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<string>(StatusCodes.Status400BadRequest);

        group.MapGet("/{articleId:guid}/likes/count",
                async Task<Ok<int>> (
                    Guid articleId,
                    GetArticleLikesCountHandler handler,
                    CancellationToken cancellationToken) =>
                {
                    var count = await handler.HandleAsync(new GetArticleLikesCountQuery
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
