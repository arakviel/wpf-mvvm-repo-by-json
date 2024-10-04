namespace UserManagar.Domain;

// POCO
internal class User : IBaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public bool IsBlocked { get; set; }
}
