using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GiftGenerator.Services;
using System.Collections.ObjectModel;

namespace GiftGenerator.Features.Home;

public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    ObservableCollection<string> chatList;

    [ObservableProperty]
    string question;

    IHttpService _httpSerive;

    public MainPageViewModel(IHttpService httpService)
    {
        _httpSerive = httpService;
        ChatList = new ObservableCollection<string>();
    }

    [RelayCommand]
    private void SendQuestion()
    {
        ChatList.Add(Question);
        Question = string.Empty;

        string resons = _httpSerive.SendMsg(Question);

        ChatList.Add(resons);
    }

}
