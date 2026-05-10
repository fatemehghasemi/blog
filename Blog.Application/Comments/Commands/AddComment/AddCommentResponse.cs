namespace Blog.Application.Comments.Commands.AddComment;

public sealed class AddCommentResponse
{
    public Guid Id { get; init; }
    public string Content { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public Guid? ParentCommentId { get; init; }
}
