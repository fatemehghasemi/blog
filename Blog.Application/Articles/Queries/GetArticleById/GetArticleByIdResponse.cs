namespace Blog.Application.Articles.Queries.GetArticleById;

public sealed class GetArticleByIdResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
