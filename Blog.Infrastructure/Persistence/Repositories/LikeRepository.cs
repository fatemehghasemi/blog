using Blog.Application.Abstractions.Persistence;
using Blog.Application.Exceptions;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.Repositories;

internal sealed class LikeRepository : ILikeRepository
{
    private readonly BlogDbContext _dbContext;

    public LikeRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Like like, CancellationToken cancellationToken)
    {
        _dbContext.Likes.Add(like);

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (IsDuplicateLike(ex))
        {
            throw new DuplicateLikeException(ex);
        }
        catch (DbUpdateException ex) when (IsInvalidReference(ex))
        {
            throw new InvalidLikeReferenceException(ex);
        }
    }

    public async Task<bool> RemoveAsync(Guid articleId, string clientId, CancellationToken cancellationToken)
    {
        var like = await _dbContext.Likes
            .FirstOrDefaultAsync(x => x.ArticleId == articleId && x.ClientId == clientId, cancellationToken);

        if (like is null)
        {
            return false;
        }

        _dbContext.Likes.Remove(like);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public Task<int> GetCountByArticleIdAsync(Guid articleId, CancellationToken cancellationToken)
    {
        return _dbContext.Likes
            .AsNoTracking()
            .CountAsync(x => x.ArticleId == articleId, cancellationToken);
    }

    private static bool IsDuplicateLike(DbUpdateException ex)
    {
        var message = ex.Message + " " + ex.InnerException?.Message;
        return message.Contains("IX_Likes_ArticleId_ClientId", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsInvalidReference(DbUpdateException ex)
    {
        var message = ex.Message + " " + ex.InnerException?.Message;
        return message.Contains("FK_Likes_Articles_ArticleId", StringComparison.OrdinalIgnoreCase);
    }
}
