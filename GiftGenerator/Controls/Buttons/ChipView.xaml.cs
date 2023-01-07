using System.Windows.Input;

namespace GiftGenerator.Controls.Buttons;

public partial class ChipView : Border
{
    public static readonly BindableProperty TapCommandProperty =
        BindableProperty.CreateAttached(nameof(TapCommand), typeof(ICommand), typeof(ChipView), null);

    public static readonly BindableProperty StrokeColorProperty =
        BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(ChipView), Colors.Blue);
    public static readonly BindableProperty BackgroundChipProperty =
        BindableProperty.Create(nameof(BackgroundChip), typeof(Color), typeof(ChipView), Colors.Blue);
    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ChipView), Colors.Blue);

    public static readonly BindableProperty NameProperty =
        BindableProperty.Create(nameof(Name), typeof(string), typeof(ChipView), "0");
    public static readonly BindableProperty IndexProperty =
        BindableProperty.Create(nameof(Index), typeof(string), typeof(ChipView), "0");

    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    public string Index
    {
        get => (string)GetValue(IndexProperty);
        set => SetValue(IndexProperty, value);
    }
    public string Name
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    public Color StrokeColor
    {
        get => (Color)GetValue(StrokeColorProperty);
        set => SetValue(StrokeColorProperty, value);
    }
    public Color BackgroundChip
    {
        get => (Color)GetValue(BackgroundChipProperty);
        set => SetValue(BackgroundChipProperty, value);
    }
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public ChipView()
	{
		InitializeComponent();
	}
}