using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Platform;
#if IOS
using UIKit;
using CoreGraphics;
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
			})
            .CustomComportament();

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddHttpClient<Services.IHttpService, Services.HttpService>();

        builder.Services.AddSingleton<Features.Home.MainPage, Features.Home.MainPageViewModel>();


        return builder.Build();
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
