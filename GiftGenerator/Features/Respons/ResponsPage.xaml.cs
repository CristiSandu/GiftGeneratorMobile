namespace GiftGenerator.Features.Respons;

public partial class ResponsPage : ContentPage
{
    public ResponsPage(ResponsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}