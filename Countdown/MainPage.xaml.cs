
using System.Timers;
using System.Net;
using System.Reflection;
using System.Diagnostics.Metrics;
using System.Security.Principal;

namespace Countdown
{
    public partial class MainPage : ContentPage
    {
        private string wordEntered;
        private char[] letter = new char [9];
        private char[] savedLetters = new char [9];
        private System.Timers.Timer timer;
        int totalTime;
        int row;
        int i = 0;
        int player1Points = 0;
        int player2Points = 0;
        int playerTurn = 1;
        int roundNum = 1;
        int saveTime = 0;
        int savePlayer1 = 0;
        int savePlayer2 = 0;

        private char[] vowelList = { 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A', 'A',
                                     'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E',
                                     'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I', 'I',
                                     'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O', 'O',
                                     'U', 'U', 'U', 'U', 'U'};
        private char[] consonantList = {'B', 'B',
                                        'C', 'C', 'C',
                                        'D', 'D', 'D', 'D', 'D', 'D', 
                                        'F', 'F',
                                        'G', 'G', 'G',
                                        'H', 'H',
                                        'J',
                                        'K',
                                        'L', 'L', 'L', 'L', 'L', 
                                        'M', 'M', 'M', 'M', 
                                        'N', 'N', 'N', 'N', 'N', 'N', 'N', 'N', 
                                        'P', 'P', 'P', 'P',
                                        'Q',
                                        'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R', 
                                        'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S', 'S', 
                                        'T', 'T', 'T', 'T', 'T', 'T', 'T', 'T', 'T', 
                                        'V',
                                        'W',
                                        'X',
                                        'Y',
                                        'Z'};
        public MainPage()
        {
            InitializeComponent();
            scoreboard();
        }

        private void ClickedSubmit(object sender, EventArgs e)
        {
            wordEntered = EnterWord.Text;

            if (wordEntered == null || wordEntered.Length == 0)
            {
                DisplayAlert("Empty answer", "Please enter a word", "Ok");
            }
            else
            {

                //Putting the letters entered into the grid
                CountDownGrid2.Clear();
                for (i = 0; i < wordEntered.Length; i++)
                {
                    letter[i] = wordEntered[i];

                    Label label = new Label();
                    label.Text = letter[i].ToString();
                    label.FontSize = 75;
                    label.HorizontalOptions = LayoutOptions.Center;
                    label.VerticalOptions = LayoutOptions.Center;
                    CountDownGrid2.Add(label, i, row);
                }
            }
            EnterWord.Text = "";

            letterCheck();
        }

        private void clearGrid()
        {
            CountDownGrid2.Clear();
            EnterWord.Text = "";
        }

        public void GiveVowel(object sender, EventArgs e) 
        {

            //Shuffles and displays one random vowel
            Random.Shared.Shuffle(vowelList);
            if(i == 9)
            {
                DisplayAlert("Limit reached", "Only 9 letters can be chosen", "Ok");
            }
            else
            {
                playerTurn = 1;

                Label label = new Label();
                label.Text = vowelList[i].ToString();
                label.FontSize = 75;
                label.HorizontalOptions = LayoutOptions.Center;
                label.VerticalOptions = LayoutOptions.Center;
                CountDownGrid.Add(label, i, row);
                savedLetters[i] = vowelList[i];
                i++;
                
                if(i == 9)
                {
                    play.IsVisible = true;
                }
            }
            
        }
        public void GiveConsonant(object sender, EventArgs e) 
        {

            //Shuffles and displays one random consonant
            Random.Shared.Shuffle(consonantList);
            if (i == 9)
            {
                DisplayAlert("Limit reached", "Only 9 letters can be chosen", "Ok");
            }
            else
            {

                playerTurn = 1;

                Label label = new Label();
                label.Text = consonantList[i].ToString();
                label.FontSize = 75;
                label.HorizontalOptions = LayoutOptions.Center;
                label.VerticalOptions = LayoutOptions.Center;
                CountDownGrid.Add(label, i, row);
                savedLetters[i] = consonantList[i];
                i++;

                if (i == 9)
                {
                    play.IsVisible = true;
                }
            }
        }

        private void playGame()
        {
            playerTurn = 1;
            i = 0;
            newRound.IsVisible = false;
            CountDownGrid2.IsVisible = true;
            EnterWord.IsVisible = true;
            SubmitEntry.IsVisible = true;
            play.IsVisible = false;
            timerLbl.IsVisible = true;
            showTurn.IsVisible = true;
            rounds.IsVisible = true;
            newRound.IsVisible = false;
            displayPlayersTurn(); 
        }

        private void displayPlayersTurn()
        {
            showTurn.Text = Players.name1 + "'s Turn";
        }

        private void stopGame()
        {
            EnterWord.IsEnabled = false;
            SubmitEntry.IsEnabled = false;
        }

        private void scoreboard()
        {
            lbl1.Text = Players.name1 + ": " + player1Points;
            lbl2.Text = Players.name2 + ": " + player2Points;
        }

        private void letterCheck()
        {
            
            //Checks if the word entered only contains the letters from the random letters
            string wordEntry = wordEntered.ToUpper();
            string lettersSaved = new string(savedLetters);
            int wordLength = 0;

            foreach (char c in wordEntry)
            {
                if (lettersSaved.Contains(c))
                {
                    wordLength++;
                }
            }

            if(wordLength == wordEntry.Length)
            {
                WordsList();
            }
            else
            {
                DisplayAlert("Incorrect use of letters", "The letters used are invalid please try again", "Ok");
            }

        }

       public void WordsList()
        {

            //Checks the text file if the word entered matches with any of the words in the text file
            string line;
            string wordEntry = wordEntered.ToUpper();

            StreamReader sr = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\..\..\Resources\WordList.txt")); //Finding the WordList.txt file starting from where the program is run
            line = sr.ReadLine();
            while(line != null)
            {
                line = sr.ReadLine();

                if (string.Equals(line, wordEntry, StringComparison.OrdinalIgnoreCase)) //Comparing strings without case sensitive
                {
                    if(playerTurn == 1)
                    {
                        DisplayAlert("Great!", wordEntered.Length + " points for " + Players.name1, "Ok");
                        player1Points += wordEntered.Length;
                        scoreboard();
                    }
                    else
                    {
                        DisplayAlert("Great!", wordEntered.Length + " points for " + Players.name2, "Ok");
                        player2Points += wordEntered.Length;
                        scoreboard();
                        checkRounds();
                    }
                    clearGrid();
                    playersTurn();
                    return;
                }
            }
            DisplayAlert("No", "Word not found", "Okay");
        }

        private void playBtn(object sender, EventArgs e)
        {
            playGame();
            startTimer();
        }

        private void startTimer()
        {
            totalTime = 30;
            timer = new System.Timers.Timer(1000); //A timer that ticks every second
            timer.Elapsed += timeElapsed;
            timer.Start();
            timerLbl.Text = $"{totalTime}'s left";
        }


        private void timeElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Dispatch(() => //Continously update the timer label
            {
                    totalTime--;
                    timerLbl.Text = $"{totalTime}'s left";

                if(totalTime == 0)
                {
                    timer.Stop();
                    timerLbl.Text = $"Times up!";

                    if(playerTurn == 1)
                    {
                        checkRounds();
                        newRound.IsVisible = true;
                        showTurn.IsVisible = false;
                        stopGame();
                    }
                    playersTurn();
                }
            }
            );
           
        }

        public void startRound(object sender, EventArgs e)
        {
            playerTurn = 0;
            roundNum++;
            rounds.Text = "Round: " + roundNum;
            CountDownGrid.Clear();
            i = 0;
            newRound.IsVisible = false;
        }

        public void playersTurn()
        {
            if(playerTurn == 1)
            {
                showTurn.Text = Players.name2 + "'s Turn";
                playerTurn--;
                return;
            }
            else
            {
                showTurn.Text = Players.name1 + "'s Turn";
                saveTime = totalTime;
                timer.Stop();
                checkRounds();
                newRound.IsVisible = true;
                timerLbl.IsVisible = false;
                showTurn.IsVisible = false;
            }
        }

        public void checkRounds()
        {
            if(roundNum > Rounds.numOfRounds)
            {
                findWinner();
            }

        }

        public void findWinner()
        {
            if (player1Points > player2Points)
            {
                stopGame();
                DisplayAlert("Player 1 has won", Players.name1 + " has won the game!", "Okay");
                savePlayer1 = player1Points;
                savePlayer2 = player2Points;
            }

            else if(player2Points > player1Points)
            {
                stopGame();
                DisplayAlert("Player 2 has won", Players.name2 + " has won the game!", "Okay");
                savePlayer1 = player1Points;
                savePlayer2 = player2Points;
            }

            else
            {
                stopGame();
                DisplayAlert("Draw", "Both players scored the same points!", "Okay");
                savePlayer1 = player1Points;
                savePlayer2 = player2Points;
            }
        }
    } 
}
