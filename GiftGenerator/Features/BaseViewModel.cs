using CommunityToolkit.Mvvm.ComponentModel;

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
}
