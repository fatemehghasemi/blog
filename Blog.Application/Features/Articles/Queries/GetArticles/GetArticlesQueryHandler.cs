using Blog.Application.Abstractions.Persistence;
using Blog.Application.DTOs;

namespace Blog.Application.Features.Articles.Queries.GetArticles;

public sealed class GetArticlesQueryHandler
{
    private readonly IArticleRepository _articleRepository;

    public GetArticlesQueryHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<IReadOnlyList<ArticleDto>> HandleAsync(GetArticlesQuery query, CancellationToken cancellationToken)
    {
        var articles = await _articleRepository.GetAllAsync(cancellationToken);

        return articles
            .Select(article => new ArticleDto
            {
                Id = article.Id,
                Title = article.Title,
                Slug = article.Slug.Value,
                CreatedAt = article.CreatedAt
            })
            .ToList();
    }
}
