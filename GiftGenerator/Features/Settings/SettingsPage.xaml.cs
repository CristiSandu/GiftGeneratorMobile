using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;

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
        avatarView.Text = Utils.Settings.UserName.Substring(0, 2).ToUpper();

    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        Utils.Settings.IsLogIn = false;

        await Shell.Current.GoToAsync("//LoginPage");
    }


    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Analytics.TrackEvent("Open Page", new Dictionary<string, string>
        {
            {"Page", this.GetType().Name },
            {"User", Utils.Settings.UserName }
        });
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Analytics.TrackEvent("Feedback", new Dictionary<string, string>
        {
            {"Content", FeedbackEditor.Text },
            {"User", Utils.Settings.UserName }
        });

        FeedbackEditor.Text = string.Empty;
    }
}