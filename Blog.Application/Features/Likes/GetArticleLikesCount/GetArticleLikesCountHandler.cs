using Blog.Application.Abstractions.Persistence;

namespace Blog.Application.Features.Likes.GetArticleLikesCount;

public sealed class GetArticleLikesCountHandler
{
    private readonly ILikeRepository _likeRepository;

    public GetArticleLikesCountHandler(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public Task<int> HandleAsync(GetArticleLikesCountQuery query, CancellationToken cancellationToken)
    {
        return _likeRepository.GetCountByArticleIdAsync(query.ArticleId, cancellationToken);
    }
}
