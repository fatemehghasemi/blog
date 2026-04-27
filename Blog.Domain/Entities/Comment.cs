namespace Blog.Domain.Entities;

public sealed class Comment
{
    public Guid Id { get; private set; }
    public Guid ArticleId { get; private set; }
    public Guid? ParentCommentId { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public bool IsApproved { get; private set; }

    private Comment()
    {
    }

    public Comment(Guid articleId, string content, Guid? parentCommentId = null)
    {
        if (articleId == Guid.Empty)
        {
            throw new ArgumentException("ArticleId is required.", nameof(articleId));
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Content is required.", nameof(content));
        }

        Id = Guid.NewGuid();
        ArticleId = articleId;
        ParentCommentId = parentCommentId;
        Content = content.Trim();
        CreatedAt = DateTime.UtcNow;
        IsApproved = true;
    }
}
