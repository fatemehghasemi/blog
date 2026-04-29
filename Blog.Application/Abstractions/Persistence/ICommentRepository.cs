using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Persistence;

public interface ICommentRepository
{
    Task AddAsync(Comment comment, CancellationToken cancellationToken);
    Task<IReadOnlyList<Comment>> GetByArticleIdAsync(Guid articleId, CancellationToken cancellationToken);
}
