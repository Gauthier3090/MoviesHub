using MoviesWorld_BLL.DTO;
using MoviesWorld_BLL.Mappers;
using MoviesWorld_DAL.Entities;
using MoviesWorld_DAL.Interfaces;

namespace MoviesWorld_BLL.Services;

public class CommentService
{
    private readonly IRepositoryComment _repositoryComment;
    private readonly IRepositoryUser _repositoryUser;


    public CommentService(IRepositoryComment repositoryComment, IRepositoryUser repositoryUser)
    {
        _repositoryComment = repositoryComment;
        _repositoryUser = repositoryUser;
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

    public IEnumerable<CommentDto> GetCommentsByPublication(int id)
    {
        IEnumerable<CommentEntity> res = _repositoryComment.GetCommentsByPublication(id);
        foreach (CommentEntity item in res) item.UserEntity = _repositoryUser.GetById(item.User);
        return res.Select(x => x.ToDto());
    }
}