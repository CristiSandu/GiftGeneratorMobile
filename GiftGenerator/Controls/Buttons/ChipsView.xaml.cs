using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GiftGenerator.Controls.Buttons;

public partial class ChipData : ObservableObject
{
    public string Index { get; set; }
    public string Name { get; set; }

    [ObservableProperty]
    Color strokeColor;

    [ObservableProperty]
    Color backgroundChip;

    public Color TextColor { get; set; }
}

public partial class ChipsView : ContentView
{
    public static readonly BindableProperty SelectionColorProperty =
  BindableProperty.Create(nameof(SelectionColor), typeof(Color), typeof(ChipsView), Colors.Blue);

    public static readonly BindableProperty UnSelectionColorProperty =
      BindableProperty.Create(nameof(UnSelectionColor), typeof(Color), typeof(ChipsView), Colors.Blue);

    public static readonly BindableProperty TextColorValueProperty =
     BindableProperty.Create(nameof(TextColorValue), typeof(Color), typeof(ChipsView), Colors.Blue);

    public static readonly BindableProperty ValuesProperty =
    BindableProperty.Create(nameof(Values), typeof(List<string>), typeof(ChipsView), null, propertyChanged: (b, o, n) => GenerateChip((ChipsView)b));

    public static readonly BindableProperty TapCommandProperty =
    BindableProperty.CreateAttached(nameof(TapCommand), typeof(ICommand), typeof(ChipsView), null);

    public ObservableCollection<ChipData> ChipValues { get; set; } = new ObservableCollection<ChipData>();

    public List<string> Values
    {
        get => (List<string>)GetValue(ValuesProperty);
        set => SetValue(ValuesProperty, value);
    }

    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    public Color SelectionColor
    {
        get => (Color)GetValue(SelectionColorProperty);
        set => SetValue(SelectionColorProperty, value);
    }

    public Color TextColorValue
    {
        get => (Color)GetValue(TextColorValueProperty);
        set => SetValue(TextColorValueProperty, value);
    }

    public Color UnSelectionColor
    {
        get => (Color)GetValue(UnSelectionColorProperty);
        set => SetValue(UnSelectionColorProperty, value);
    }

    public int LastSelection { get; set; } = 0;

    public Command<string> Command { get; private set; }


    public ChipsView()
    {
        Command = new Command<string>(indexString =>
        {
            if (!int.TryParse(indexString, out int index))
                return;

            if (LastSelection == index)
            {
                ChipValues[index].StrokeColor = UnSelectionColor;
                ChipValues[index].BackgroundChip = UnSelectionColor;
                LastSelection = -1;
                return;
            }

            if (LastSelection != -1)
            {
                ChipValues[index].StrokeColor = SelectionColor;
                ChipValues[index].BackgroundChip = SelectionColor;
                ChipValues[LastSelection].StrokeColor = UnSelectionColor;
                ChipValues[LastSelection].BackgroundChip = UnSelectionColor;

                LastSelection = index;
            }
            else
            {
                ChipValues[index].StrokeColor = SelectionColor;
                ChipValues[index].BackgroundChip = SelectionColor;

                LastSelection = index;
            }

            TapCommand.Execute(ChipValues[index].Name);
        });

        InitializeComponent();
        GenerateChip(this);
    }

    private static void GenerateChip(ChipsView content)
    {
        if (content.Values == null)
            return;

        if (content.Values.Count > 1)
            content.ChipValues.Add(new ChipData
            {
                Index = "0",
                Name = "All",
                TextColor = content.TextColorValue,
                StrokeColor = content.SelectionColor,
                BackgroundChip = content.SelectionColor,
            });

        int index = content.Values.Count == 1 ? 0 : 1;

        content.Values.ForEach(teams =>
        {
            content.ChipValues.Add(new ChipData
            {
                Index = $"{index}",
                Name = teams,
                TextColor = content.TextColorValue,
                StrokeColor = index != 0 ? content.UnSelectionColor : content.SelectionColor,
                BackgroundChip = index != 0 ? content.UnSelectionColor : content.SelectionColor,
            });

            index++;
        });
    }
}