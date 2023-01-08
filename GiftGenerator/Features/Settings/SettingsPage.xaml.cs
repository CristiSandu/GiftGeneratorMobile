namespace GiftGenerator.Features.Settings;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        userNameLabel.Text = Utils.Settings.UserName;
        avatarView.Text = Utils.Settings.UserName.Substring(0,2).ToUpper();

    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        Utils.Settings.IsLogIn = false;

        await Shell.Current.GoToAsync("//LoginPage");
    }
}