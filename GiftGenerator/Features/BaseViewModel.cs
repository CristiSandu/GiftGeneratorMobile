using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using System.Diagnostics;

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


    public Stopwatch watch = new();

    [RelayCommand]
    private void OnNavigateTo()
    {
        watch.Start();
        Analytics.TrackEvent("Open Page", new Dictionary<string, string>
        {
            {"Page", this.GetType().Name },
            {"User", Utils.Settings.UserName }

        });
    }

    [RelayCommand]
    private void OnNavigateFrom()
    {
        watch.Stop();
        Analytics.TrackEvent("TimeOnPage", new Dictionary<string, string>
        {
            {"Time", this.GetType().Name + "-" + (watch.ElapsedMilliseconds/1000).ToString() },
            {"User", Utils.Settings.UserName }
        });
    }


    [RelayCommand]
    private async void GoTo(string page)
    {
        await Shell.Current.GoToAsync(page);
    }
}
