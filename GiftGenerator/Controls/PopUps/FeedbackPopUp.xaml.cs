using CommunityToolkit.Maui.Views;
using Microsoft.AppCenter.Analytics;

namespace GiftGenerator.Controls.PopUps;

public partial class FeedbackPopUp : Popup
{
	public FeedbackPopUp()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        Button btn = sender as Button;

        if (btn.Text == "Cancel")
        {
            Close();
            return;
        }

        Analytics.TrackEvent("Feedback", new Dictionary<string, string>
        {
            {"Content", feedbackEntry.Text },
            {"User", Utils.Settings.UserName }
        });

        feedbackEntry.Text = string.Empty;
        Close();
    }
}