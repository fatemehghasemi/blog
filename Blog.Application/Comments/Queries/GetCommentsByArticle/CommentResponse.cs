namespace Blog.Application.Comments.Queries.GetCommentsByArticle;

public sealed class CommentResponse
{
    public Guid Id { get; init; }
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public Guid? ParentCommentId { get; init; }
}
