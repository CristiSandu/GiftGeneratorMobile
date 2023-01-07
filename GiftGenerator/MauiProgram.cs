using CommunityToolkit.Maui;
using GiftGenerator.Features.Login;
using GiftGenerator.Features.Respons;
using GiftGenerator.Services;
using GiftGenerator.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;
#if IOS
using UIKit;
using CoreGraphics;
using Plugin.Firebase.Auth;
using Plugin.Firebase.Shared;
using GiftGenerator.Services.Interfaces;
#endif

#if IOS
using Plugin.Firebase.iOS;
#elif ANDROID
using Plugin.Firebase.Android;
using Plugin.Firebase.Auth;
using Plugin.Firebase.Shared;
#endif

namespace GiftGenerator;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Poppins-Medium-500.ttf", "PPM");
                fonts.AddFont("Poppins-SemiBold-600.ttf", "PPSB");
            })
            .RegisterFirebaseServices()
            .CustomComportament();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddHttpClient<IHttpService, HttpService>();
        builder.Services.AddSingleton<IPredictionService, PredictionService>();
        builder.Services.AddSingleton<IAuthService, AuthService>();

        builder.Services.AddSingleton<Features.Home.MainPage, Features.Home.MainPageViewModel>();
        builder.Services.AddSingleton<LoginPage, LoginPageViewModel>();
        builder.Services.AddTransient<ResponsPage, ResponsPageViewModel>();

        return builder.Build();
    }

    private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
    {
        builder.ConfigureLifecycleEvents(events =>
        {
#if IOS
            events.AddiOS(iOS => iOS.FinishedLaunching((app, launchOptions) => {
                CrossFirebase.Initialize(app, launchOptions, CreateCrossFirebaseSettings());
                return false;
            }));
#elif ANDROID
            events.AddAndroid(android => android.OnCreate((activity, state) =>
                CrossFirebase.Initialize(activity, state, CreateCrossFirebaseSettings())));
#endif
        });

        builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
        return builder;
    }

    private static CrossFirebaseSettings CreateCrossFirebaseSettings()
    {
        return new CrossFirebaseSettings(isAuthEnabled: true, googleRequestIdToken: "251679508411-v3iauj839aloq62svrkl4ggl2likmkc6.apps.googleusercontent.com");
    }

    private static MauiAppBuilder CustomComportament(this MauiAppBuilder builder)
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("BorderlessEntry", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Microsoft.Maui.Graphics.Colors.Transparent.ToPlatform());
#elif IOS
            handler.PlatformView.BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent.ToPlatform();
            handler.PlatformView.Layer.BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent.ToCGColor();
            UIView paddingView = new UIView(new CGRect(0, 0, 10.0f, 20.0f));
            handler.PlatformView.LeftView = paddingView;
            handler.PlatformView.LeftViewMode = UITextFieldViewMode.Always;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
        });
        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("BorderlessEditor", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Microsoft.Maui.Graphics.Colors.Transparent.ToPlatform());
#elif IOS
            handler.PlatformView.BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent.ToPlatform();
            handler.PlatformView.Layer.BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent.ToCGColor();
#endif
        });
        Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("BorderlessDatePicker", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Microsoft.Maui.Graphics.Colors.Transparent.ToPlatform());
            handler.PlatformView.SetPadding(0, 0, 0, 0);
#elif IOS
            handler.PlatformView.BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent.ToPlatform();
            handler.PlatformView.Layer.BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent.ToCGColor();
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
            UIView paddingView = new UIView(new CGRect(0, 0, 10.0f, 20.0f));
            handler.PlatformView.LeftView = paddingView;
            handler.PlatformView.LeftViewMode = UITextFieldViewMode.Always;
#endif
        });
        Microsoft.Maui.Handlers.TimePickerHandler.Mapper.AppendToMapping("BorderlessTimePicker", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Microsoft.Maui.Graphics.Colors.Transparent.ToPlatform());
#elif IOS
            handler.PlatformView.BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent.ToPlatform();
            handler.PlatformView.Layer.BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent.ToCGColor();
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
        });
        return builder;
    }
}
