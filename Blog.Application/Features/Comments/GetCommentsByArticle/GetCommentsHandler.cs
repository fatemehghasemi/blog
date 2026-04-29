using Blog.Application.Abstractions.Persistence;
using Blog.Application.DTOs;

namespace Blog.Application.Features.Comments.GetCommentsByArticle;

public sealed class GetCommentsHandler
{
    private readonly ICommentRepository _commentRepository;

    public GetCommentsHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IReadOnlyList<CommentDto>> HandleAsync(GetCommentsQuery query, CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetByArticleIdAsync(query.ArticleId, cancellationToken);

        return comments.Select(comment => new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                ParentCommentId = comment.ParentCommentId
            })
            .ToList();
    }
}
