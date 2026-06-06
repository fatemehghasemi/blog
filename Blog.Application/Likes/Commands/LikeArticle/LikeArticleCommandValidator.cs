using FluentValidation;

namespace Blog.Application.Likes.Commands.LikeArticle;

public sealed class LikeArticleCommandValidator : AbstractValidator<LikeArticleCommand>
{
    public LikeArticleCommandValidator()
    {
        RuleFor(command => command.ArticleId)
            .NotEmpty().WithMessage("ArticleId is required.");

        RuleFor(command => command.ClientId)
            .NotEmpty().WithMessage("ClientId is required.")
            .MaximumLength(200).WithMessage("ClientId must not exceed 200 characters.");
    }
}
