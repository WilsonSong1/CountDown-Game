namespace Countdown;

public partial class Page1 : ContentPage
{
	public Page1()
	{
		InitializeComponent();
	}

	private async void clickPlay(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Players());
	}

	private async void clickHistory(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new History());
    }
    private async void clickSettings(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Settings());
    }
    private void clickExit(object sender, EventArgs e)
    {
		System.Environment.Exit(0);
    }

}