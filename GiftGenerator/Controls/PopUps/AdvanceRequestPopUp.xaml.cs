using CommunityToolkit.Maui.Views;

namespace GiftGenerator.Controls.PopUps;

public partial class AdvanceRequestPopUp : Popup
{
    public AdvanceRequestPopUp()
    {
        InitializeComponent();
        BindingContext = new AdvanceRequestPopUpViewModel();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Button btn = sender as Button;

        if (btn.Text == "Cancel")
        {
            Close();
            return;
        }

        if ((BindingContext as AdvanceRequestPopUpViewModel).CustomRequest != null)
        {
            (BindingContext as AdvanceRequestPopUpViewModel).RequestAdvancedRecomandationCommand.Execute(null);
            Close();
        }
    }
}