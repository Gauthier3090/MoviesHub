using MoviesWorld_BLL.Mappers;
using MoviesWorld_DAL.Entities;
using MoviesWorld_DAL.Interfaces;
using MoviesWorld_BLL.DTO;


namespace MoviesWorld_BLL.Services;

public class PublicationService
{
    private readonly IRepositoryPublication _repositoryPublication;

    public PublicationService(IRepositoryPublication repositoryPublication)
    {
        _repositoryPublication = repositoryPublication;
    }

    public PublicationDto? Insert(string title, string description, string image, int creator)
    {
        int id = _repositoryPublication.Insert(new PublicationEntity
        {
            Title = title,
            Description = description,
            Image = image,
            Creator = creator,
        });
        return _repositoryPublication.GetById(id)?.ToDto();
    }

    public List<PublicationDto> GetPublicationByUser(int creator)
    {
        return _repositoryPublication.GetPublicationByUser(creator).Select(x => x.ToDto()).ToList();
    }
}