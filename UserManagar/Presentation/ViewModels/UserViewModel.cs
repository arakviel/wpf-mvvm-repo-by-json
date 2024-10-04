using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using UserManagar.Application.Dtos;
using UserManagar.Application.Services.Contracts;
using UserManagar.Presentation.Utils;

namespace UserManagar.Presentation.ViewModels;

internal class UserViewModel : BindableBase
{
    private readonly IUserService _userService;

    public UserViewModel(IUserService userService)
    {
        _userService = userService;
        LoadUsers();
        SaveCommand = new RelayCommand(Save);
        UpdateCommand = new RelayCommand(Update, CanUpdate);
        DeleteCommand = new RelayCommand(Delete, CanDelete);
    }

    // Властивості для прив'язки даних
    private ObservableCollection<UserViewModel> _users = new ObservableCollection<UserViewModel>();
    public ObservableCollection<UserViewModel> Users
    {
        get => _users;
        set => SetProperty(ref _users, value);
    }

    private UserViewModel? _selectedUserViewModel;
    public UserViewModel? SelectedUserViewModel
    {
        get => _selectedUserViewModel;
        set
        {
            SetProperty(ref _selectedUserViewModel, value);
            UpdateCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
            if (value != null)
            {
                Name = value.Name;
                Password = value.Password;
                Age = value.Age;
            }
        }
    }

    private int _id;
    public int Id
    {
        get => _id; private set
        {
            _id = Users.Count + 1;
        }
    }

    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private string _password;
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    private int _age;
    public int Age
    {
        get => _age;
        set => SetProperty(ref _age, value);
    }

    public RelayCommand SaveCommand { get; }
    public RelayCommand UpdateCommand { get; }
    public RelayCommand DeleteCommand { get; }

    private void LoadUsers()
    {
        var users = _userService.FindAll();
        Users.Clear();
        foreach (var user in users)
        {
            Users.Add(new UserViewModel(_userService)
            {
                Name = user.Name,
                Password = user.Password,
                Age = user.Age
            });
        }
    }

    private void Save(object? obj)
    {
        var createUserDto = new CreateUserDto(Name, Password, Age);
        var validationResults = Validate(createUserDto);

        if (validationResults.Count == 0)
        {
            _userService.save(createUserDto);
            MessageBox.Show("Користувача успішно створено.");
            LoadUsers(); // Перезавантаження списку
        }
        else
        {
            ShowValidationErrors(validationResults);
        }
    }

    private void Update(object? obj)
    {
        if (SelectedUserViewModel == null) return;

        var updateUserDto = new UpdateUserDto
        {
            Id = SelectedUserViewModel.Id, // Отримайте Id від вибраного користувача
            Name = Name,
            Password = Password,
            Age = Age
        };
        var validationResults = Validate(updateUserDto);

        if (validationResults.Count == 0)
        {
            _userService.update(updateUserDto);
            MessageBox.Show("Користувача успішно оновлено.");
            LoadUsers(); // Перезавантаження списку
        }
        else
        {
            ShowValidationErrors(validationResults);
        }
    }

    private void Delete(object? obj)
    {
        if (SelectedUserViewModel == null) return;

        int userId = SelectedUserViewModel.Id; // Отримайте Id від вибраного користувача
        _userService.delete(userId);
        MessageBox.Show("Користувача успішно видалено.");
        LoadUsers(); // Перезавантаження списку
    }

    private bool CanUpdate(object? obj)
    {
        return SelectedUserViewModel != null;
    }

    private bool CanDelete(object? obj)
    {
        return SelectedUserViewModel != null;
    }

    private IList<ValidationResult> Validate(object dto)
    {
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(dto, context, results, true);
        return results;
    }

    private void ShowValidationErrors(IList<ValidationResult> validationResults)
    {
        foreach (var validationResult in validationResults)
        {
            MessageBox.Show(validationResult.ErrorMessage);
        }
    }
}
