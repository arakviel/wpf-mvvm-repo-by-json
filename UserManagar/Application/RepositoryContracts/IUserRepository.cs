using UserManagar.Domain;

namespace UserManagar.Application.RepositoryContracts;

internal interface IUserRepository : IRepository<User>
{
    User? getOneByName(string name);
}
