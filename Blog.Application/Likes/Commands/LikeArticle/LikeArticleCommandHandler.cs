using Blog.Application.Abstractions.Persistence;
using Blog.Domain.Entities;

namespace Blog.Application.Likes.Commands.LikeArticle;

public sealed class LikeArticleCommandHandler
{
    private readonly ILikeRepository _likeRepository;

    public LikeArticleCommandHandler(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public async Task<LikeArticleResponse> HandleAsync(LikeArticleCommand command, CancellationToken cancellationToken)
    {
        var like = Like.Create(command.ArticleId, command.ClientId);
        await _likeRepository.AddAsync(like, cancellationToken);

        return new LikeArticleResponse
        {
            ArticleId = like.ArticleId,
            ClientId = like.ClientId,
            CreatedAt = like.CreatedAt
        };
    }
}
