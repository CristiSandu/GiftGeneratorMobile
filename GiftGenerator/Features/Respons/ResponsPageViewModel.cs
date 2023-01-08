
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GiftGenerator.Controls.PopUps;
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

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Initials))]
    string userName;

    public string Initials => UserName[..2].ToUpper();

    public ResponsPageViewModel()
    {
        Recomandations = new List<string>();
        UserName = Utils.Settings.UserName;
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
        await UtilsMethods.ShowToast($"Copied in clipboard\n{value}", CommunityToolkit.Maui.Core.ToastDuration.Short);
        SelectedRecommandation = null;
    }

    [RelayCommand]
    private async void OpenRequestPopUp()
    {
        var popup = new AdvanceRequestPopUp();
        var result = await Shell.Current.ShowPopupAsync(popup);
    }
}
