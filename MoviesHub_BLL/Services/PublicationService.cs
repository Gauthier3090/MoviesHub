using MoviesHub_BLL.DTO;
using MoviesHub_BLL.Mappers;
using MoviesHub_DAL.Entities;
using MoviesHub_DAL.Interfaces;


namespace MoviesHub_BLL.Services;

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

    public List<PublicationDto>? GetPublicationByUser(int creator)
    {
        return _repositoryPublication.GetPublicationByUser(creator).Select(x => x.ToDto()).ToList();
    }
}