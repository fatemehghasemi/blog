using Blog.Application.Abstractions.Persistence;

namespace Blog.Application.Features.Likes.UnlikeArticle;

public sealed class UnlikeArticleHandler
{
    private readonly ILikeRepository _likeRepository;

    public UnlikeArticleHandler(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public Task<bool> HandleAsync(UnlikeArticleCommand command, CancellationToken cancellationToken)
    {
        return _likeRepository.RemoveAsync(command.ArticleId, command.ClientId, cancellationToken);
    }
}
