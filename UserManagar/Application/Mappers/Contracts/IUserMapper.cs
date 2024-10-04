using UserManagar.Application.Dtos;
using UserManagar.Domain;

namespace UserManagar.Application.Mappers.Contracts;

internal interface IUserMapper
{
    User ToUser(CreateUserDto createUserDto);
    CreateUserDto ToCreateUserDto(User user);
    User ToUser(UpdateUserDto updateUserDto, User existingUser);
    UpdateUserDto ToUpdateUserDto(User user);
}