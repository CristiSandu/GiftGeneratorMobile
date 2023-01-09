using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GiftGenerator.Features.Respons;
using GiftGenerator.Services.Interfaces;
using Microsoft.AppCenter.Analytics;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace GiftGenerator.Features.Home;

public partial class MainPageViewModel : BaseViewModel
{
    [ObservableProperty]
    List<string> persons = new List<string>
    {
        "Girlfriend",
        "Boyfriend"
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
        "0-100 lei",
        "200-400 lei",
        "400-800 lei",
        "800-1200 lei",
        "1200-2000 lei",
        "2000-3000 lei"
    };

    [ObservableProperty]
    bool isGirl = true;

    [ObservableProperty]
    string person;

    [ObservableProperty]
    string priceInterval;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Initials))]
    string userName;


    public string Initials => UserName[..2].ToUpper();

    [ObservableProperty]
    List<string> selectedIntervals;


    IHttpService _httpSerive;
    IPredictionService _predictionService;

    public MainPageViewModel(IHttpService httpService, IPredictionService predictionService)
    {
        _httpSerive = httpService;
        _predictionService = predictionService;
        SelectedIntervals = new List<string>();
        UserName = Utils.Settings.UserName;
    }

    [RelayCommand]
    private void PersonChouse(string person)
    {
        Person = person;
        IsGirl = person == "girlfriend";
    }

    [RelayCommand]
    private void PriceChouse(string price)
    {
        PriceInterval = price;
    }

    [RelayCommand]
    private void InteresList(List<string> selections)
    {
        SelectedIntervals = selections;
    }

    [RelayCommand]
    private async void GeneratePosiblePrompts()
    {
        //List<string> finalPrompts = new List<string>();

        //foreach (var person in persons)
        //{
        //    List<string> intervale = new List<string>();
        //    for (int i = 0; i < interests.Count; i++)
        //    {
        //        var concatRange = string.Join(",", interests.Take(i + 1));
        //        intervale.Add(concatRange);

        //        finalPrompts.Add($"Give me 10 gift ideas for my {person} witch have thease interests: {interests[i]}");
        //    }

        //    foreach (string interest in intervale)
        //    {
        //        finalPrompts.Add($"Give me 10 gift ideas for my {person} witch have thease interests: {interest}");
        //    }
        //}

        //var serializedJsonRequest = JsonConvert.SerializeObject(finalPrompts);

        //int j = 0;

        Analytics.TrackEvent("Selected Interests", new Dictionary<string, string>
        {
            {"Interesets", string.Join(", ", SelectedIntervals) },
            {"For", IsGirl ? "Girlfriend" : "Boyfriend" },
            {"Prices", PriceInterval},
            {"User", Utils.Settings.UserName }
        });

        IsBusy = true;
        string fileName = IsGirl ? "InterestsG.json" : "InterestsB.json";
        List<string> respons = await _predictionService.GetRecomandationBasedOfInterests(fileName, SelectedIntervals);

        string person = IsGirl ? "Girlfriend" : "Boyfriend";
        var navigationParameter = new Dictionary<string, object>
        {
            { "Recomandations", respons },
            { "Message", $"{Person}\nInterested in: {string.Join(", ", SelectedIntervals)}\nPrice Interval {PriceInterval}" }
        };

        await Task.Delay(3000);

        IsBusy = false;
        await Shell.Current.GoToAsync(nameof(ResponsPage), navigationParameter);
    }

    //[RelayCommand]
    //private void SendQuestion()
    //{
    //    ChatList.Add(Question);
    //    Question = string.Empty;

    //    string resons = _httpSerive.SendMsg(Question);

    //    ChatList.Add(resons);
    //}
}
