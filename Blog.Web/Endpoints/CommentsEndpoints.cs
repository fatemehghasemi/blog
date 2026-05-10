using Blog.Application.Comments.Commands.AddComment;
using Blog.Application.Comments.Queries.GetCommentsByArticle;
using Blog.Application.Exceptions;
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
                async Task<Results<Created<AddCommentResponse>, BadRequest<string>, Conflict<string>>> (
                    Guid articleId,
                    AddCommentRequest request,
                    AddCommentCommandHandler commandHandler,
                    ICommandExecutionPipeline commandPipeline,
                    CancellationToken cancellationToken) =>
                {
                    try
                    {
                        var response = await commandPipeline.ExecuteAsync(
                            ct => commandHandler.HandleAsync(new AddCommentCommand
                            {
                                ArticleId = articleId,
                                Content = request.Content,
                                ParentCommentId = request.ParentCommentId
                            }, ct),
                            cancellationToken);

                        return TypedResults.Created($"/api/articles/{articleId}/comments/{response.Id}", response);
                    }
                    catch (ArgumentException ex)
                    {
                        return TypedResults.BadRequest(ex.Message);
                    }
                    catch (InvalidCommentReferenceException ex)
                    {
                        return TypedResults.Conflict(ex.Message);
                    }
                })
            .WithName("CreateComment")
            .Produces<AddCommentResponse>(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status409Conflict);

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
