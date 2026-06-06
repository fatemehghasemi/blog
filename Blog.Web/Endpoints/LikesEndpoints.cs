using Blog.Application.Interfaces;
using Blog.Application.Likes.Commands.LikeArticle;
using Blog.Application.Likes.Commands.UnlikeArticle;
using Blog.Application.Likes.Queries.GetArticleLikesCount;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Endpoints;

public static class LikesEndpoints
{
    public static IEndpointRouteBuilder MapLikesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/articles")
            .WithTags("Likes");

        group.MapPost("/{articleId:guid}/like",
                async Task<Created<LikeArticleResponse>> (
                    Guid articleId,
                    [FromHeader(Name = "X-Client-Id")] string clientId,
                    LikeArticleCommandHandler commandHandler,
                    ICommandExecutionPipeline commandPipeline,
                    CancellationToken cancellationToken) =>
                {
                    var command = new LikeArticleCommand
                    {
                        ArticleId = articleId,
                        ClientId = clientId
                    };

                    var response = await commandPipeline.ExecuteAsync(
                        command,
                        (cmd, ct) => commandHandler.HandleAsync(cmd, ct),
                        cancellationToken);

                    return TypedResults.Created($"/api/articles/{articleId}/like", response);
                })
            .WithName("LikeArticle")
            .Produces<LikeArticleResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapDelete("/{articleId:guid}/like",
                async Task<NoContent> (
                    Guid articleId,
                    [FromHeader(Name = "X-Client-Id")] string clientId,
                    UnlikeArticleCommandHandler commandHandler,
                    ICommandExecutionPipeline commandPipeline,
                    CancellationToken cancellationToken) =>
                {
                    var command = new UnlikeArticleCommand
                    {
                        ArticleId = articleId,
                        ClientId = clientId
                    };

                    await commandPipeline.ExecuteAsync(
                        command,
                        (cmd, ct) => commandHandler.HandleAsync(cmd, ct),
                        cancellationToken);

                    return TypedResults.NoContent();
                })
            .WithName("UnlikeArticle")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest);

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
}
