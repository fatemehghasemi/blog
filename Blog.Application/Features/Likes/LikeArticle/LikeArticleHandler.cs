using Blog.Application.Abstractions.Persistence;
using Blog.Application.DTOs;
using Blog.Domain.Entities;

namespace Blog.Application.Features.Likes.LikeArticle;

public sealed class LikeArticleHandler
{
    private readonly ILikeRepository _likeRepository;

    public LikeArticleHandler(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public async Task<LikeResponse> HandleAsync(LikeArticleCommand command, CancellationToken cancellationToken)
    {
        var like = new Like(command.ArticleId, command.ClientId);
        await _likeRepository.AddAsync(like, cancellationToken);

        return new LikeResponse
        {
            ArticleId = like.ArticleId,
            ClientId = like.ClientId,
            CreatedAt = like.CreatedAt
        };
    }
}
