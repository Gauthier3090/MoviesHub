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

    public CommentDto? Insert(string headline, string body, int userId, int publication)
    {
        UserDto? user = _repositoryUser.GetById(userId)?.ToDto();
        if (user is null)
        {
            throw new ArgumentOutOfRangeException(nameof(userId));
        }

        int id = _repositoryComment.Insert(new CommentEntity
        {
            Headline = headline,
            Body = body,
            User = userId,
            Publication = publication
        });

        return _repositoryComment.GetById(id)?.ToDto(user);
    }

    public IEnumerable<CommentDto> GetCommentsByPublication(int id)
    {
        IEnumerable<CommentEntity> res = _repositoryComment.GetCommentsByPublication(id);


        foreach (CommentEntity item in res)
        { 
            UserDto user = _repositoryUser.GetById(item.User)!.ToDto();
            CommentDto comment = item.ToDto(user);
       

            yield return comment;
        }
    }
}