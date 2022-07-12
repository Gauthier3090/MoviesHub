using MoviesWorld_BLL.Mappers;
using MoviesWorld_DAL.Entities;
using MoviesWorld_DAL.Interfaces;
using MoviesWorld_BLL.DTO;

namespace MoviesWorld_BLL.Services;

public class FollowService
{
    private readonly IRepositoryFollow _repositoryFollow;
    private readonly IRepositoryUser _repositoryUser;

    public FollowService(IRepositoryFollow repositoryFollow, IRepositoryUser repositoryUser)
    {
        _repositoryFollow = repositoryFollow;
        _repositoryUser = repositoryUser;
    }

    public FollowDto? Insert(int target, int follow)
    {
        UserDto targetDto = _repositoryUser.GetById(target)!.ToDto();
        UserDto followDto = _repositoryUser.GetById(follow)!.ToDto();
        int id = _repositoryFollow.Insert(new FollowEntity
        {
            Target = target,
            Follower = follow
        });
        return _repositoryFollow.GetById(id)?.ToDto(targetDto, followDto);
    }
}