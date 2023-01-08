﻿using GiftGenerator.Features.Home;
using GiftGenerator.Features.Respons;
using GiftGenerator.Features.Settings;

namespace GiftGenerator;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(ResponsPage), typeof(ResponsPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}
