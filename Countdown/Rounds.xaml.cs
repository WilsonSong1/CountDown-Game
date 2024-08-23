namespace Countdown;

public partial class Rounds : ContentPage
{
	int wholeValue;
	public static int numOfRounds;

    public Rounds()
	{
		InitializeComponent();
	}

	public void slide(object sender, ValueChangedEventArgs args)
	{
		double value = args.NewValue;
		wholeValue = (int)value;
		roundNumber.Text = String.Format("Number of rounds: {0}", wholeValue);
	}

	public void roundSet()
	{
		numOfRounds = wholeValue;
	}
}