using UserManagar.Application.Dtos;
using UserManagar.Application.Mappers.Contracts;
using UserManagar.Domain;

namespace UserManagar.Application.Mappers;

internal class UserMapper : IUserMapper
{
    public User ToUser(CreateUserDto createUserDto)
    {
        return new User
        {
            Name = createUserDto.Name,
            Password = createUserDto.Password,
            Age = createUserDto.Age,
            IsBlocked = false // За замовчуванням новий користувач не заблокований
        };
    }

    public CreateUserDto ToCreateUserDto(User user)
    {
        return new CreateUserDto(user.Name, user.Password, user.Age);
    }

    public User ToUser(UpdateUserDto updateUserDto, User existingUser)
    {
        existingUser.Name = updateUserDto.Name ?? existingUser.Name;
        existingUser.Password = updateUserDto.Password ?? existingUser.Password;
        existingUser.Age = updateUserDto.Age != default ? updateUserDto.Age : existingUser.Age;
        existingUser.IsBlocked = updateUserDto.IsBlocked;

        return existingUser;
    }

    public UpdateUserDto ToUpdateUserDto(User user)
    {
        return new UpdateUserDto
        {
            Id = user.Id,
            Name = user.Name,
            Password = user.Password,
            Age = user.Age,
            IsBlocked = user.IsBlocked
        };
    }
}
