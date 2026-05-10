using Blog.Application.Abstractions.Persistence;
using Blog.Domain.Entities;

namespace Blog.Application.Articles.Commands.CreateArticle;

public sealed class CreateArticleCommandHandler
{
    private readonly IArticleRepository _articleRepository;

    public CreateArticleCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<CreateArticleResponse> HandleAsync(CreateArticleCommand command, CancellationToken cancellationToken)
    {
        var article = Article.Create(command.Title, command.Content);
        await _articleRepository.AddAsync(article, cancellationToken);

        return new CreateArticleResponse
        {
            Id = article.Id,
            Slug = article.Slug.Value
        };
    }
}
