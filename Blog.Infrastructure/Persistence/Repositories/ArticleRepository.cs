using Blog.Application.Abstractions.Persistence;
using Blog.Application.Exceptions;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.Repositories;

internal sealed class ArticleRepository : IArticleRepository
{
    private readonly BlogDbContext _dbContext;

    public ArticleRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Article article, CancellationToken cancellationToken)
    {
        _dbContext.Articles.Add(article);

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (IsSlugConstraintViolation(ex))
        {
            throw new DuplicateSlugException(article.Slug.Value, ex);
        }
    }

    public async Task<IReadOnlyList<Article>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Articles
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    private static bool IsSlugConstraintViolation(DbUpdateException ex)
    {
        return ex.Message.Contains("IX_Articles_Slug", StringComparison.OrdinalIgnoreCase)
               || (ex.InnerException?.Message.Contains("IX_Articles_Slug", StringComparison.OrdinalIgnoreCase) ?? false);
    }
}
