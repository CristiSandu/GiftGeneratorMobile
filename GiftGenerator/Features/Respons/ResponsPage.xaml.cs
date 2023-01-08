namespace GiftGenerator.Features.Respons;

public partial class ResponsPage : ContentPage
{
    public ResponsPage(ResponsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        (BindingContext as ResponsPageViewModel).NavigateToCommand.Execute(null);
    }
}