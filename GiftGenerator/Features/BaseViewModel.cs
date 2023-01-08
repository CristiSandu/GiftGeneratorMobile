using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GiftGenerator.Features;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    string title;

    [ObservableProperty]
    bool waitForAction;

    public bool IsNotBusy => !IsBusy;

    [RelayCommand]
    private async void GoTo(string page)
    {
        await Shell.Current.GoToAsync(page);
    }
}
