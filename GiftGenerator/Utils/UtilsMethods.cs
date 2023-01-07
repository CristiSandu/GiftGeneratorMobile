using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace GiftGenerator.Utils;

public static class UtilsMethods
{
    public static async Task ShowToast(string message, ToastDuration duration)
    {
        CancellationTokenSource cancellationTokenSource = new();

        var toast = Toast.Make(message, duration);
        await toast.Show(cancellationTokenSource.Token);
    }
}
