using MoviesWorld_BLL.Mappers;
using MoviesWorld_DAL.Entities;
using MoviesWorld_DAL.Interfaces;
using MoviesWorld_BLL.DTO;


namespace MoviesWorld_BLL.Services;

public class PublicationService
{
    private readonly IRepositoryPublication _repositoryPublication;
    private readonly IRepositoryUser _repositoryUser;

    public PublicationService(IRepositoryPublication repositoryPublication, IRepositoryUser repositoryUser)
    {
        _repositoryPublication = repositoryPublication;
        _repositoryUser = repositoryUser;
    }

    public PublicationDto? Insert(string title, string description, string image, int creator)
    {
        UserDto? user = _repositoryUser.GetById(creator)?.ToDto();
        if (user is null)
        {
            throw new ArgumentOutOfRangeException(nameof(creator));
        }
        int id = _repositoryPublication.Insert(new PublicationEntity
        {
            Title = title,
            Description = description,
            Image = image,
            Creator = creator,
        });
        return _repositoryPublication.GetById(id)?.ToDto(user);
    }

    public IEnumerable<PublicationDto> GetPublicationByUser(int creator)
    {
        UserDto? user = _repositoryUser.GetById(creator)?.ToDto();
        if (user is null)
        {
            throw new ArgumentOutOfRangeException(nameof(creator));
        }
        return _repositoryPublication.GetPublicationByUser(creator).Select(x => x.ToDto(user));
    }

    public string? PublicationExist(int id)
    {
        return _repositoryPublication.PublicationExist(id);
    }
}