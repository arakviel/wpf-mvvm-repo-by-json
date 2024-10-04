using System.ComponentModel.DataAnnotations;

namespace UserManagar.Application.Dtos;

public record UpdateUserDto
{
    [Required(ErrorMessage = "Поле Id є обов'язковим.")]
    public int Id { get; init; }

    [StringLength(100, ErrorMessage = "Ім'я не повинно перевищувати 100 символів.")]
    public string Name { get; init; }

    [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль повинен бути від 6 до 100 символів.")]
    public string Password { get; init; }

    [Range(18, 100, ErrorMessage = "Вік повинен бути між 18 та 100 роками.")]
    public int Age { get; init; }

    public bool IsBlocked { get; init; }
}