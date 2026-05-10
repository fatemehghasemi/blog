using Blog.Application.Abstractions.Persistence;

namespace Blog.Application.Articles.Queries.GetArticleById;

public sealed class GetArticleByIdQueryHandler
{
    private readonly IArticleRepository _articleRepository;

    public GetArticleByIdQueryHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<GetArticleByIdResponse?> HandleAsync(GetArticleByIdQuery query, CancellationToken cancellationToken)
    {
        var articles = await _articleRepository.GetAllAsync(cancellationToken);
        var article = articles.FirstOrDefault(x => x.Id == query.Id);

        if (article is null)
        {
            return null;
        }

        return new GetArticleByIdResponse
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            Slug = article.Slug.Value,
            CreatedAt = article.CreatedAt
        };
    }
}
