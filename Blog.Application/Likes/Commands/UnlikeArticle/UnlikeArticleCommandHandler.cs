using Blog.Application.Abstractions.Persistence;

namespace Blog.Application.Likes.Commands.UnlikeArticle;

public sealed class UnlikeArticleCommandHandler
{
    private readonly ILikeRepository _likeRepository;

    public UnlikeArticleCommandHandler(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public Task<bool> HandleAsync(UnlikeArticleCommand command, CancellationToken cancellationToken)
    {
        return _likeRepository.RemoveAsync(command.ArticleId, command.ClientId, cancellationToken);
    }
}
