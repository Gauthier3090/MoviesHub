using MoviesWorld_BLL.Mappers;
using MoviesWorld_DAL.Entities;
using MoviesWorld_DAL.Interfaces;
using MoviesWorld_BLL.DTO;

namespace MoviesWorld_BLL.Services;

public class CommentService
{
    private readonly IRepositoryComment _repositoryComment;

    public CommentService(IRepositoryComment repositoryComment)
    {
        _repositoryComment = repositoryComment;
    }

    public CommentDto? Insert(string headline, string body, int user, int publication)
    {
        int id = _repositoryComment.Insert(new CommentEntity
        {
            Headline = headline,
            Body = body,
            User = user,
            Publication = publication
        });
        return _repositoryComment.GetById(id)?.ToDto();
    }
}