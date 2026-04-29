namespace Blog.Application.Features.Comments.CreateComment;

public sealed class CreateCommentCommand
{
    public Guid ArticleId { get; init; }
    public string Content { get; init; } = string.Empty;
    public Guid? ParentCommentId { get; init; }
}
