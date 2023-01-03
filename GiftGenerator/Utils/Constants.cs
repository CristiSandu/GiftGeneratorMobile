using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftGenerator.Utils;

public static class Constants
{
    public static string OPENAI_API_KEY = "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";
    public static double ScreenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
    public static double ScreenHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
}
