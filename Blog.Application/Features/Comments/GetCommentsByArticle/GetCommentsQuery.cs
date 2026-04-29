namespace Blog.Application.Features.Comments.GetCommentsByArticle;

public sealed class GetCommentsQuery
{
    public Guid ArticleId { get; init; }
}
