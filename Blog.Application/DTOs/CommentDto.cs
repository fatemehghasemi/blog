namespace Blog.Application.DTOs;

public sealed class CommentDto
{
    public Guid Id { get; init; }
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public Guid? ParentCommentId { get; init; }
}
