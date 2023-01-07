
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GiftGenerator.Utils;

namespace GiftGenerator.Features.Respons;

public partial class ResponsPageViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ResponsCount))]
    List<string> recomandations;

    [ObservableProperty]
    string message;

    [ObservableProperty]
    string customRecomandation;

    [ObservableProperty]
    int responsCount;

    [ObservableProperty]
    string selectedRecommandation;


    public ResponsPageViewModel()
    {
        Recomandations = new List<string>();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var recom = query["Recomandations"] as List<string>;
        Message = query["Message"] as string;
        ResponsCount = recom.Count;
        Recomandations.AddRange(recom);


    }

    partial void OnSelectedRecommandationChanged(string value)
    {
        if (value != null)
            CopyRecommandationCommand.Execute(value);
    }

    [RelayCommand]
    private async void CopyRecommandation(string value)
    {
        await Clipboard.Default.SetTextAsync(value);
        await UtilsMethods.ShowToast("Recommendation Copied in clipboard", CommunityToolkit.Maui.Core.ToastDuration.Short);
        SelectedRecommandation = null;
    }

    [RelayCommand]
    private async void RequestAdvancedRecomandation()
    {
        string subject = "Advanced Recomandation Request";
        string body = CustomRecomandation;
        string[] recipients = new[] { "cristysandu3@gmail.com" };

        var message = new EmailMessage
        {
            Subject = subject,
            Body = body,
            BodyFormat = EmailBodyFormat.Html,
            To = new List<string>(recipients)
        };

        await Email.Default.ComposeAsync(message);
    }
}
