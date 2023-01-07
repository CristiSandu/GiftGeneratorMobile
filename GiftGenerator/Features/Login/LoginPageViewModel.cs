
using CommunityToolkit.Mvvm.Input;
using GiftGenerator.Features.Home;
using GiftGenerator.Services.Interfaces;

namespace GiftGenerator.Features.Login;

public partial class LoginPageViewModel : BaseViewModel
{
    private IAuthService _authService;
    public LoginPageViewModel(IAuthService authService)
    {
        _authService = authService;
    }

    [RelayCommand]
    private async void Login()
    {
        try
        {

            var user = await _authService.SignInWithGoogle();
        }
        catch (Exception ex)
        {

        }
        int i = 0;

        await Shell.Current.GoToAsync($"{nameof(MainPage)}");
    }
}
