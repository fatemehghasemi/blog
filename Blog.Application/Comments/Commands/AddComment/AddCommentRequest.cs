namespace Blog.Application.Comments.Commands.AddComment;

public sealed class AddCommentRequest
{
    public string Content { get; init; } = string.Empty;
    public Guid? ParentCommentId { get; init; }
}
