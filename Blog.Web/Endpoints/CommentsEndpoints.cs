using Blog.Application.Comments.Commands.AddComment;
using Blog.Application.Comments.Queries.GetCommentsByArticle;
using Blog.Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Blog.Web.Endpoints;

public static class CommentsEndpoints
{
    public static IEndpointRouteBuilder MapCommentsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/articles/{articleId:guid}/comments")
            .WithTags("Comments");

        group.MapPost("/",
                async Task<Created<AddCommentResponse>> (
                    Guid articleId,
                    AddCommentRequest request,
                    AddCommentCommandHandler commandHandler,
                    ICommandExecutionPipeline commandPipeline,
                    CancellationToken cancellationToken) =>
                {
                    var command = new AddCommentCommand
                    {
                        ArticleId = articleId,
                        Content = request.Content,
                        ParentCommentId = request.ParentCommentId
                    };

                    var response = await commandPipeline.ExecuteAsync(
                        command,
                        (cmd, ct) => commandHandler.HandleAsync(cmd, ct),
                        cancellationToken);

                    return TypedResults.Created($"/api/articles/{articleId}/comments/{response.Id}", response);
                })
            .WithName("CreateComment")
            .Produces<AddCommentResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapGet("/",
                async Task<Ok<IReadOnlyList<CommentResponse>>> (
                    Guid articleId,
                    GetCommentsByArticleQueryHandler queryHandler,
                    CancellationToken cancellationToken) =>
                {
                    var response = await queryHandler.HandleAsync(new GetCommentsByArticleQuery
                    {
                        ArticleId = articleId
                    }, cancellationToken);

                    return TypedResults.Ok(response);
                })
            .WithName("GetCommentsByArticle")
            .Produces<IReadOnlyList<CommentResponse>>(StatusCodes.Status200OK);

        return app;
    }
}
