using System.Linq.Expressions;
using UserManagar.Application.Dtos;
using UserManagar.Domain;

namespace UserManagar.Application.Services.Contracts;

internal interface IUserService
{
    User? FindOneById(int id);
    IEnumerable<User> FindAll();
    IEnumerable<User> FindAll(Expression<Func<User, bool>> filter);
    void save(CreateUserDto createUserDto);
    void update(UpdateUserDto updateUserDto);
    void delete(int id);
}
