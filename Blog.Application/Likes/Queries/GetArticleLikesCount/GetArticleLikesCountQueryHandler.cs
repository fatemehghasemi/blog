using Blog.Application.Abstractions.Persistence;

namespace Blog.Application.Likes.Queries.GetArticleLikesCount;

public sealed class GetArticleLikesCountQueryHandler
{
    private readonly ILikeRepository _likeRepository;

    public GetArticleLikesCountQueryHandler(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public Task<int> HandleAsync(GetArticleLikesCountQuery query, CancellationToken cancellationToken)
    {
        return _likeRepository.GetCountByArticleIdAsync(query.ArticleId, cancellationToken);
    }
}
