namespace Blog.Application.Articles.Queries.Shared;

public sealed class ArticleSummaryResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
