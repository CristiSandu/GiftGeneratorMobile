using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GiftGenerator.Features;
using Microsoft.AppCenter.Analytics;

namespace GiftGenerator.Controls.PopUps;

public partial class AdvanceRequestPopUpViewModel : BaseViewModel
{
    [ObservableProperty]
    string customRequest;

    public AdvanceRequestPopUpViewModel()
    {

    }

    [RelayCommand]
    private async void RequestAdvancedRecomandation()
    {
        Analytics.TrackEvent("Send on AdvanceRequest", new Dictionary<string, string>
        {
            {"CustomRequest", CustomRequest },
            {"User", Utils.Settings.UserName }
        });

        string subject = "Advanced Recomandation Request";
        string body = CustomRequest;
        string[] recipients = new[] { "cristysandu3@gmail.com" };

        var message = new EmailMessage
        {
            Subject = subject,
            Body = body,
            BodyFormat = EmailBodyFormat.PlainText,
            To = new List<string>(recipients)
        };

        await Email.Default.ComposeAsync(message);
    }
}
