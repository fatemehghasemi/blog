using Blog.Application.Abstractions.Persistence;
using Blog.Application.Articles.Queries.Shared;

namespace Blog.Application.Articles.Queries.GetArticlesList;

public sealed class GetArticlesListQueryHandler
{
    private readonly IArticleRepository _articleRepository;

    public GetArticlesListQueryHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<IReadOnlyList<ArticleSummaryResponse>> HandleAsync(GetArticlesListQuery query, CancellationToken cancellationToken)
    {
        var articles = await _articleRepository.GetAllAsync(cancellationToken);

        return articles
            .Select(article => new ArticleSummaryResponse
            {
                Id = article.Id,
                Title = article.Title,
                Slug = article.Slug.Value,
                CreatedAt = article.CreatedAt
            })
            .ToList();
    }
}
