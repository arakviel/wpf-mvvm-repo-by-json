using System.Linq.Expressions;
using UserManagar.Domain;

namespace UserManagar.Application.RepositoryContracts;

internal interface IRepository<T> where T : IBaseEntity
{
    T? FindOneById(int id);
    IEnumerable<T> FindAll();
    IEnumerable<T> FindAll(Expression<Func<T, bool>> filter);
    void save(T entity);
    void update(T entity);
    void delete(int id);
}
