using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;

namespace GiftGenerator;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override void OnStart()
    {
        base.OnStart();
        AppCenter.Start("android=a4800e12-413f-49b0-b18e-61a57b741acf;",
                   typeof(Analytics));
    }
}
