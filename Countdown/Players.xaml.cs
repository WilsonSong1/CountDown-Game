namespace Countdown;

public partial class Players : ContentPage
{
    public static string name1;
    public static string name2;
	public Players()
	{
		InitializeComponent();
	}

    public void clickSubmit(object sender, EventArgs e)
    {
        name1 = Player1Name.Text;
        name2 = Player2Name.Text;

        if (name1 != null && name2 != null)
        {
            Navigation.PushAsync(new MainPage());
        }
        else if (name1 == null)
        {
            DisplayAlert("Empty name", "Please Enter a name for Player 1", "Ok");
        }
        else
        {
            DisplayAlert("Empty name", "Please Enter a name for Player 2", "Ok");
        }
    }
}