using FluentValidation;

namespace Blog.Application.Likes.Commands.UnlikeArticle;

public sealed class UnlikeArticleCommandValidator : AbstractValidator<UnlikeArticleCommand>
{
    public UnlikeArticleCommandValidator()
    {
        RuleFor(command => command.ArticleId)
            .NotEmpty().WithMessage("ArticleId is required.");

        RuleFor(command => command.ClientId)
            .NotEmpty().WithMessage("ClientId is required.")
            .MaximumLength(200).WithMessage("ClientId must not exceed 200 characters.");
    }
}
