using Blog.Domain.Entities;

namespace Blog.Application.Abstractions.Persistence;

public interface IArticleRepository
{
    Task AddAsync(Article article, CancellationToken cancellationToken);
    Task<IReadOnlyList<Article>> GetAllAsync(CancellationToken cancellationToken);
}
