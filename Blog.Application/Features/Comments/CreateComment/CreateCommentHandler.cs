using Blog.Application.Abstractions.Persistence;
using Blog.Application.DTOs;
using Blog.Domain.Entities;

namespace Blog.Application.Features.Comments.CreateComment;

public sealed class CreateCommentHandler
{
    private readonly ICommentRepository _commentRepository;

    public CreateCommentHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<CommentDto> HandleAsync(CreateCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = new Comment(command.ArticleId, command.Content, command.ParentCommentId);
        await _commentRepository.AddAsync(comment, cancellationToken);

        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
            ParentCommentId = comment.ParentCommentId
        };
    }
}
