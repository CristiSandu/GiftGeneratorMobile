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

    public static readonly BindableProperty MultipleSelectionProperty =
BindableProperty.Create(nameof(MultipleSelection), typeof(bool), typeof(ChipsView), false);

    public static readonly BindableProperty ListSpanProperty =
BindableProperty.Create(nameof(ListSpan), typeof(int), typeof(ChipsView), 1);

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

    public bool MultipleSelection
    {
        get => (bool)GetValue(MultipleSelectionProperty);
        set => SetValue(MultipleSelectionProperty, value);
    }

    public int ListSpan
    {
        get => (int)GetValue(ListSpanProperty);
        set => SetValue(ListSpanProperty, value);
    }

    public int LastSelection { get; set; } = -1;

    public Command<string> Command { get; private set; }

    List<string> SelectedValues = new List<string>();


    public ChipsView()
    {
        Command = new Command<string>(indexString =>
        {
            if (!MultipleSelection)
            {
                if (!int.TryParse(indexString, out int index))
                    return;

                if (LastSelection == index)
                {
                    ChipValues[index].StrokeColor = UnSelectionColor;
                    ChipValues[index].BackgroundChip = UnSelectionColor;
                    LastSelection = -1;
                    TapCommand.Execute(null);
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
            }
            else
            {
                if (!int.TryParse(indexString, out int index))
                    return;

                if (ChipValues[index].StrokeColor == UnSelectionColor)
                {
                    ChipValues[index].StrokeColor = SelectionColor;
                    ChipValues[index].BackgroundChip = SelectionColor;
                    SelectedValues.Add(ChipValues[index].Name);

                }
                else
                {
                    ChipValues[index].StrokeColor = UnSelectionColor;
                    ChipValues[index].BackgroundChip = UnSelectionColor;
                    SelectedValues.Remove(ChipValues[index].Name);
                }

                TapCommand.Execute(SelectedValues);
            }

        });

        InitializeComponent();
        GenerateChip(this);
    }

    private static void GenerateChip(ChipsView content)
    {
        if (content.Values == null)
            return;


        int index = 0;

        content.Values.ForEach(teams =>
        {
            content.ChipValues.Add(new ChipData
            {
                Index = $"{index}",
                Name = teams,
                TextColor = content.TextColorValue,
                StrokeColor = content.UnSelectionColor,
                BackgroundChip = content.UnSelectionColor,
            });

            index++;
        });
    }
}