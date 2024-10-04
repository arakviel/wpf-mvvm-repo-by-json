using UserManagar.Application.RepositoryContracts;
using UserManagar.Domain;

namespace UserManagar.Infrastructure.Persistence;


internal class UserFileRepository : BaseFileRepository<User>, IUserRepository
{
    public UserFileRepository() : base("Users.json") { }

    public User? getOneByName(string name)
    {
        var allUsers = FindAll().ToList();
        return allUsers.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}
