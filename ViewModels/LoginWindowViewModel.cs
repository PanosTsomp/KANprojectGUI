using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KANprojectGUI.Managers;
using KANprojectGUI.Models;

namespace KANprojectGUI.ViewModels;

public partial class LoginWindowViewModel(User user) : ViewModelBase
{
    [ObservableProperty]
    private string _name = user.Name;

    [ObservableProperty]
    private string _password = user.Password;

    [ObservableProperty]
    private string _email = user.Email;

    [ObservableProperty]
    private string _statusMessage;

    [RelayCommand]
    private void Login()
    {
        var isValid = DbManager.Instance.UserExists(Name, Password, Email);

        StatusMessage = isValid
            ? "Login successful!"
            : "Login failed. Please check your credentials.";
    }
}
