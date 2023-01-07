using GiftGenerator.Features.Home;

namespace GiftGenerator;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}
