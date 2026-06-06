using Blog.Application.Articles.Commands.CreateArticle;
using Blog.Application.Articles.Queries.GetArticleById;
using Blog.Application.Articles.Queries.GetArticlesList;
using Blog.Application.Articles.Queries.Shared;
using Blog.Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Blog.Web.Endpoints;

public static class ArticlesEndpoints
{
    public static IEndpointRouteBuilder MapArticlesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/articles")
            .WithTags("Articles");

        group.MapPost("/",
                async Task<Created<CreateArticleResponse>> (
                    CreateArticleRequest request,
                    CreateArticleCommandHandler commandHandler,
                    ICommandExecutionPipeline commandPipeline,
                    CancellationToken cancellationToken) =>
                {
                    var command = new CreateArticleCommand
                    {
                        Title = request.Title,
                        Content = request.Content
                    };

                    var response = await commandPipeline.ExecuteAsync(
                        command,
                        (cmd, ct) => commandHandler.HandleAsync(cmd, ct),
                        cancellationToken);

                    return TypedResults.Created($"/api/articles/{response.Id}", response);
                })
            .WithName("CreateArticle")
            .Produces<CreateArticleResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapGet("/",
                async Task<Ok<IReadOnlyList<ArticleSummaryResponse>>> (
                    GetArticlesListQueryHandler queryHandler,
                    CancellationToken cancellationToken) =>
                {
                    var response = await queryHandler.HandleAsync(new GetArticlesListQuery(), cancellationToken);
                    return TypedResults.Ok(response);
                })
            .WithName("GetArticlesList")
            .Produces<IReadOnlyList<ArticleSummaryResponse>>(StatusCodes.Status200OK);

        group.MapGet("/{id:guid}",
                async Task<Results<Ok<GetArticleByIdResponse>, NotFound>> (
                    Guid id,
                    GetArticleByIdQueryHandler queryHandler,
                    CancellationToken cancellationToken) =>
                {
                    var response = await queryHandler.HandleAsync(new GetArticleByIdQuery
                    {
                        Id = id
                    }, cancellationToken);

                    return response is null ? TypedResults.NotFound() : TypedResults.Ok(response);
                })
            .WithName("GetArticleById")
            .Produces<GetArticleByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}
