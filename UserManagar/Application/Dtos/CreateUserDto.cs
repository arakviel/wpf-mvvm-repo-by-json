using System.ComponentModel.DataAnnotations;

namespace UserManagar.Application.Dtos;

internal record CreateUserDto(
    [Required(ErrorMessage = "Ім'я обов'язкове")]
    [StringLength(100, ErrorMessage = "Назва повинна містити від 2 до 100 символів", MinimumLength = 2)]
    string Name,

    [Required(ErrorMessage = "Потрібен пароль")]
    [StringLength(50, ErrorMessage = "Пароль повинен містити від 8 до 50 символів", MinimumLength = 8)]
    string Password,

    [Range(18, 120, ErrorMessage = "Вік повинен бути від 18 до 120 років")]
    int Age
);