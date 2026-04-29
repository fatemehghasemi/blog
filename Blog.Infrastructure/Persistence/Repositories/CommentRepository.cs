using Blog.Application.Abstractions.Persistence;
using Blog.Application.Exceptions;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.Repositories;

internal sealed class CommentRepository : ICommentRepository
{
    private readonly BlogDbContext _dbContext;

    public CommentRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Comment comment, CancellationToken cancellationToken)
    {
        _dbContext.Comments.Add(comment);

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (IsReferenceViolation(ex))
        {
            throw new InvalidCommentReferenceException(ex);
        }
    }

    public async Task<IReadOnlyList<Comment>> GetByArticleIdAsync(Guid articleId, CancellationToken cancellationToken)
    {
        return await _dbContext.Comments
            .AsNoTracking()
            .Where(x => x.ArticleId == articleId && x.IsApproved)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    private static bool IsReferenceViolation(DbUpdateException ex)
    {
        var message = ex.Message + " " + ex.InnerException?.Message;
        return message.Contains("FK_Comments_Articles_ArticleId", StringComparison.OrdinalIgnoreCase)
               || message.Contains("FK_Comments_Comments_ParentCommentId", StringComparison.OrdinalIgnoreCase);
    }
}
