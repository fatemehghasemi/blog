using FluentValidation;

namespace Blog.Application.Articles.Commands.CreateArticle;

public sealed class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(command => command.Content)
            .NotEmpty().WithMessage("Content is required.");
    }
}
