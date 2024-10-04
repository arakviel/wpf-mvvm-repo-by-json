using System.IO;
using System.Linq.Expressions;
using System.Text.Json;
using UserManagar.Application.RepositoryContracts;
using UserManagar.Domain;

namespace UserManagar.Infrastructure.Persistence;

internal class BaseFileRepository<T> : IRepository<T> where T : class, IBaseEntity
{
    private readonly string _fileName;
    private readonly string _filePath;

    public BaseFileRepository(string fileName)
    {
        _fileName = fileName;
        _filePath = Path.Combine("Data", _fileName);

        // Якщо папка не існує, створюємо її
        if (!Directory.Exists("Data"))
        {
            Directory.CreateDirectory("Data");
        }

        // Якщо файл не існує, створюємо порожній файл
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]"); // Порожній JSON-масив
        }
    }

    public T? FindOneById(int id)
    {
        var allEntities = FindAll().ToList();
        return allEntities.FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<T> FindAll()
    {
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> filter)
    {
        var allEntities = FindAll();
        return allEntities.AsQueryable().Where(filter).ToList();
    }

    public void save(T entity)
    {
        var allEntities = FindAll().ToList();
        allEntities.Add(entity);
        SaveAll(allEntities);
    }

    public void update(T entity)
    {
        var allEntities = FindAll().ToList();
        var index = allEntities.FindIndex(e => e.Id == entity.Id);

        if (index != -1)
        {
            allEntities[index] = entity;
            SaveAll(allEntities);
        }
        else
        {
            throw new Exception($"Entity with Id {entity.Id} not found.");
        }
    }

    public void delete(int id)
    {
        var allEntities = FindAll().ToList();
        var entityToRemove = allEntities.FirstOrDefault(e => e.Id == id);

        if (entityToRemove != null)
        {
            allEntities.Remove(entityToRemove);
            SaveAll(allEntities);
        }
        else
        {
            throw new Exception($"Entity with Id {id} not found.");
        }
    }

    private void SaveAll(IEnumerable<T> entities)
    {
        var json = JsonSerializer.Serialize(entities);
        File.WriteAllText(_filePath, json);
    }
}

