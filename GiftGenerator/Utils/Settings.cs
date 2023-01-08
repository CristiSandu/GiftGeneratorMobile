
namespace GiftGenerator.Utils;

public static class Settings
{
    const int theme = 1;
    const string userName = "unknow";
    const string email = "unknow";
    const bool isLogIn = false;

    public static int Theme
    {
        get => Preferences.Get(nameof(Theme), theme);
        set => Preferences.Set(nameof(Theme), value);
    }

    public static string UserName
    {
        get => Preferences.Get(nameof(UserName), userName);
        set => Preferences.Set(nameof(UserName), value);
    }

    public static string Email
    {
        get => Preferences.Get(nameof(Email), email);
        set => Preferences.Set(nameof(Email), value);
    }

    public static bool IsLogIn
    {
        get => Preferences.Get(nameof(IsLogIn), isLogIn);
        set => Preferences.Set(nameof(IsLogIn), value);
    }

    public static void SetTheme()
    {
        switch (Theme)
        {
            // default
            case 0:
                App.Current.UserAppTheme = AppTheme.Unspecified;
                break;
            // light
            case 1:
                App.Current.UserAppTheme = AppTheme.Light;
                break;
            // dark
            case 2:
                App.Current.UserAppTheme = AppTheme.Dark;
                break;
        }
    }
}
