using System.Linq.Expressions;
using UserManagar.Application.Dtos;
using UserManagar.Application.Mappers.Contracts;
using UserManagar.Application.RepositoryContracts;
using UserManagar.Application.Services.Contracts;
using UserManagar.Domain;

namespace UserManagar.Application.Services;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;

    public UserService(IUserRepository userRepository, IUserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public User? FindOneById(int id)
    {
        return _userRepository.FindOneById(id);
    }

    public IEnumerable<User> FindAll()
    {
        return _userRepository.FindAll();
    }

    public IEnumerable<User> FindAll(Expression<Func<User, bool>> filter)
    {
        return _userRepository.FindAll(filter);
    }

    public void save(CreateUserDto createUserDto)
    {
        var user = _userMapper.ToUser(createUserDto);
        _userRepository.save(user);
    }

    public void update(UpdateUserDto updateUserDto)
    {
        var existingUser = _userRepository.FindOneById(updateUserDto.Id);
        if (existingUser == null)
        {
            throw new Exception($"User with Id {updateUserDto.Id} not found.");
        }

        var updatedUser = _userMapper.ToUser(updateUserDto, existingUser);
        _userRepository.update(updatedUser);
    }

    public void delete(int id)
    {
        _userRepository.delete(id);
    }
}
