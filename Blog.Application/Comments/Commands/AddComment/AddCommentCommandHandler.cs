using Blog.Application.Abstractions.Persistence;
using Blog.Domain.Entities;

namespace Blog.Application.Comments.Commands.AddComment;

public sealed class AddCommentCommandHandler
{
    private readonly ICommentRepository _commentRepository;

    public AddCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<AddCommentResponse> HandleAsync(AddCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = new Comment(command.ArticleId, command.Content, command.ParentCommentId);
        await _commentRepository.AddAsync(comment, cancellationToken);

        return new AddCommentResponse
        {
            Id = comment.Id,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            ParentCommentId = comment.ParentCommentId
        };
    }
}
