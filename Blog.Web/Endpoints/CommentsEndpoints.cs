using Blog.Application.DTOs;
using Blog.Application.Exceptions;
using Blog.Application.Features.Comments.CreateComment;
using Blog.Application.Features.Comments.GetCommentsByArticle;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Blog.Web.Endpoints;

public static class CommentsEndpoints
{
    public static IEndpointRouteBuilder MapCommentsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/articles/{articleId:guid}/comments")
            .WithTags("Comments");

        group.MapPost("/",
                async Task<Results<Created<CommentDto>, BadRequest<string>, Conflict<string>>> (
                    Guid articleId,
                    CreateCommentRequest request,
                    CreateCommentHandler handler,
                    CancellationToken cancellationToken) =>
                {
                    try
                    {
                        var response = await handler.HandleAsync(new CreateCommentCommand
                        {
                            ArticleId = articleId,
                            Content = request.Content,
                            ParentCommentId = request.ParentCommentId
                        }, cancellationToken);

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
            .Produces<CommentDto>(StatusCodes.Status201Created)
            .Produces<string>(StatusCodes.Status400BadRequest)
            .Produces<string>(StatusCodes.Status409Conflict);

        group.MapGet("/",
                async Task<Ok<IReadOnlyList<CommentDto>>> (
                    Guid articleId,
                    GetCommentsHandler handler,
                    CancellationToken cancellationToken) =>
                {
                    var response = await handler.HandleAsync(new GetCommentsQuery
                    {
                        ArticleId = articleId
                    }, cancellationToken);

                    return TypedResults.Ok(response);
                })
            .WithName("GetCommentsByArticle")
            .Produces<IReadOnlyList<CommentDto>>(StatusCodes.Status200OK);

        return app;
    }
}
