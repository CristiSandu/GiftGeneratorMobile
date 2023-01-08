using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GiftGenerator.Features.Respons;
using GiftGenerator.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace GiftGenerator.Features.Home;

public partial class MainPageViewModel : BaseViewModel
{
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
    bool isGirl = true;

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
        IsBusy = true;
        string fileName = IsGirl ? "InterestsG.json" : "InterestsB.json";
        List<string> respons = await _predictionService.GetRecomandationBasedOfInterests(fileName, SelectedIntervals);

        string person = IsGirl ? "Girlfriend" : "Boyfriend";
        var navigationParameter = new Dictionary<string, object>
        {
            { "Recomandations", respons },
            { "Message", $"{person}\nInterested in: {string.Join(", ", SelectedIntervals)}\nLess then {PriceInterval}" }
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
