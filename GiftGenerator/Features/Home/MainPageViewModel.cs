using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GiftGenerator.Services;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace GiftGenerator.Features.Home;

public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    ObservableCollection<string> chatList;

    [ObservableProperty]
    List<string> persons = new List<string>
    {
        "girlfriend",
        "boyfriend"
    };

    [ObservableProperty]
    List<string> interests = new List<string>
    {
        "Sport"
        ,"Creative"
        ,"Nature"
        ,"Lively"
        ,"Fashionable"
        ,"Gadget"
        ,"Hobbyist"
        ,"DIY"
        ,"House"
        ,"Intellectual"
        ,"Adventurous"
        ,"Hippy"
        ,"Indoor"
        ,"Outdoor"
    };



    [ObservableProperty]
    List<string> interval = new List<string>
    {
        "50 lei",
        "100 lei",
        "150 lei",
        "200 lei",
        "500 lei",
        "2000 lei"
    };

    [ObservableProperty]
    string question;

    IHttpService _httpSerive;

    public MainPageViewModel(IHttpService httpService)
    {
        _httpSerive = httpService;
        ChatList = new ObservableCollection<string>();
    }

    [RelayCommand]
    private void PersonChouse(string person)
    {
        int i = 0;
    }

    [RelayCommand]
    private void InteresList(List<string> selections)
    {
        int i = 0;
    }

    [RelayCommand]
    private void GeneratePosiblePrompts()
    {
        List<string> finalPrompts = new List<string>();

        foreach (var person in persons)
        {
            List<string> intervale = new List<string>();
            for (int i = 0; i < interests.Count; i++)
            {
                var concatRange = string.Join(",", interests.Take(i + 1));
                intervale.Add(concatRange);

                finalPrompts.Add($"Give me 10 gift ideas for my {person} witch have thease interests: {interests[i]}");
            }

            foreach (string interest in intervale)
            {
                finalPrompts.Add($"Give me 10 gift ideas for my {person} witch have thease interests: {interest}");
            }


        }

        var serializedJsonRequest = JsonConvert.SerializeObject(finalPrompts);

        int j = 0;

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
