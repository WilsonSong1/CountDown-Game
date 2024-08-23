namespace Countdown;

public partial class Timer : ContentPage
{
	int wholeValue;
	public static int numOfSeconds;

    public Timer()
	{
        InitializeComponent();
    }

	public void time(object sender, ValueChangedEventArgs args)
	{
		double value = args.NewValue;
		wholeValue = (int)value;
        timerNumber.Text = String.Format("Number of seconds: {0}", wholeValue);
	}

	public async void secondsSet(object sender, EventArgs e)
	{
		numOfSeconds = wholeValue;
        await Navigation.PushAsync(new Page1());
    }
}