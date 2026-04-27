using Blog.Application.Abstractions.Persistence;
using Blog.Application.DTOs;
using Blog.Domain.Entities;

namespace Blog.Application.Features.Articles.Commands.CreateArticle;

public sealed class CreateArticleCommandHandler
{
    private readonly IArticleRepository _articleRepository;

    public CreateArticleCommandHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<CreateArticleResponse> HandleAsync(CreateArticleCommand command, CancellationToken cancellationToken)
    {
        var article = new Article(command.Title, command.Content);

        await _articleRepository.AddAsync(article, cancellationToken);

        return new CreateArticleResponse
        {
            Id = article.Id,
            Slug = article.Slug.Value
        };
    }
}
