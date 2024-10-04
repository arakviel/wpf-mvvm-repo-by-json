using System.Windows;
using UserManagar.Application.Mappers;
using UserManagar.Application.Services;
using UserManagar.Infrastructure.Persistence;
using UserManagar.Presentation.ViewModels;

namespace UserManagar.Presentation;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var userService = new UserService(new UserFileRepository(), new UserMapper());
        DataContext = new UserViewModel(userService);
    }
}