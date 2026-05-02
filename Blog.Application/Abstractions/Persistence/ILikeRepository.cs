using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Persistence;

public interface ILikeRepository
{
    Task AddAsync(Like like, CancellationToken cancellationToken);
    Task<bool> RemoveAsync(Guid articleId, string clientId, CancellationToken cancellationToken);
    Task<int> GetCountByArticleIdAsync(Guid articleId, CancellationToken cancellationToken);
}
