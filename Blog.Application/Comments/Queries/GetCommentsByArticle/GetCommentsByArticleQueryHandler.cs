using Blog.Application.Abstractions.Persistence;

namespace Blog.Application.Comments.Queries.GetCommentsByArticle;

public sealed class GetCommentsByArticleQueryHandler
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentsByArticleQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IReadOnlyList<CommentResponse>> HandleAsync(GetCommentsByArticleQuery query, CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetByArticleIdAsync(query.ArticleId, cancellationToken);

        return comments.Select(comment => new CommentResponse
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                ParentCommentId = comment.ParentCommentId
            })
            .ToList();
    }
}
