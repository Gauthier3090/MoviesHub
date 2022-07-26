using MoviesWorld_DAL.Entities;

namespace MoviesWorld_DAL.Interfaces;

public interface IRepositoryFollow : IRepository<int, FollowEntity>
{
    public IEnumerable<FollowEntity> GetFollows(int target);
    public int? FollowerExist(int target, int follower);
    public bool DeleteUser(int target, int follower);
}