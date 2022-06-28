﻿using MoviesHub_BLL.DTO;
using MoviesHub_BLL.Mappers;
using MoviesHub_DAL.Entities;
using MoviesHub_DAL.Interfaces;

namespace MoviesHub_BLL.Services;

public class UserService
{
    private readonly IRepositoryUser _repositoryUser;

    public UserService(IRepositoryUser repositoryUser)
    {
        _repositoryUser = repositoryUser;
    }

    public UserDto Insert(string email, string firstName, string lastName, string password, int old)
    {
        int id = _repositoryUser.Insert(new UserEntity
        {
            Email = email,
            Password = password,
            Firstname = firstName,
            Lastname = lastName,
            Old = old
        });
        return _repositoryUser.GetById(id).ToDto();
    }

    public bool Delete(int id)
    {
        return _repositoryUser.Delete(id);
    }
}