using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GiftGenerator.Features;

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
