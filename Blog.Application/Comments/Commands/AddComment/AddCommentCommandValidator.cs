using FluentValidation;

namespace Blog.Application.Comments.Commands.AddComment;

public sealed class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
{
    public AddCommentCommandValidator()
    {
        RuleFor(command => command.ArticleId)
            .NotEmpty().WithMessage("ArticleId is required.");

        RuleFor(command => command.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(2000).WithMessage("Content must not exceed 2000 characters.");

        RuleFor(command => command.ParentCommentId)
            .NotEmpty().WithMessage("ParentCommentId must not be an empty GUID.")
            .When(command => command.ParentCommentId.HasValue);
    }
}
