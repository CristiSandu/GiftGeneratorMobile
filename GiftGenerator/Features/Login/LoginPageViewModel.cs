
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GiftGenerator.Features.Home;
using GiftGenerator.Services.Interfaces;

namespace GiftGenerator.Features.Login;

public partial class LoginPageViewModel : BaseViewModel
{
    private IAuthService _authService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNameVisible))]
    string registerText;

    [ObservableProperty]
    string email;

    [ObservableProperty]
    string password;

    [ObservableProperty]
    string displayName;

    public bool IsNameVisible => RegisterText == "Back";

    public LoginPageViewModel(IAuthService authService)
    {
        _authService = authService;
        RegisterText = "Register";
        Title = "Login";
    }

    [RelayCommand]
    private async void Login()
    {
        if (RegisterText == "Back")
        {
            if (string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(DisplayName))
            {

                await Shell.Current.DisplayAlert("Alert!", $"Complet all fields", "Ok");
                return;
            }

            bool respons = await _authService.RegisterUserTappedAsync(Email, Password, DisplayName);
            if (respons)
                RegisterText = "Register";
            else
                await Shell.Current.DisplayAlert("Alert!", $"Error apear, try again", "Ok");
        }
        else
        {
            if (string.IsNullOrEmpty(Email) ||
               string.IsNullOrEmpty(Password))
            {
                await Shell.Current.DisplayAlert("Alert!", $"Complet all fields", "Ok");
                return;
            }

            bool respons = await _authService.LoginBtnTappedAsync(Email, Password);
            if (respons)
                await Shell.Current.GoToAsync($"{nameof(MainPage)}");
            else
                await Shell.Current.DisplayAlert("Alert!", $"Error apear, try again", "Ok");
        }
    }

    [RelayCommand]
    private async void RegisterSelected()
    {
        if (RegisterText == "Back")
        {
            RegisterText = "Register";
            Title = "Login";

        }
        else
        {
            Title = "Register";
            RegisterText = "Back";
        }
    }

}
