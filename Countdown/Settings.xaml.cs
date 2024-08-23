namespace Countdown;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}

    private async void returnClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Page1());
    }

    private async void roundsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Rounds());
    }

    private async void timerClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Timer());
    }

    private void darkClicked(object sender, EventArgs e)
    {

    }
}