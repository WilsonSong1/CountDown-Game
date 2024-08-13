﻿
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
            Random.Shared.Shuffle(vowelList);
            if(i == 9)
            {
                DisplayAlert("Limit reached", "Only 9 letters can be chosen", "Ok");
            }
            else
            {
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
            Random.Shared.Shuffle(consonantList);
            if (i == 9)
            {
                DisplayAlert("Limit reached", "Only 9 letters can be chosen", "Ok");
            }
            else
            {
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
            CountDownGrid2.IsVisible = true;
            EnterWord.IsVisible = true;
            SubmitEntry.IsVisible = true;
            play.IsVisible = false;
            timerLbl.IsVisible = true;
        }

        private void scoreboard()
        {
            lbl1.Text = Players.name1 + ": " + player1Points;
            lbl2.Text = Players.name2 + ": " + player2Points;
        }

        private void letterCheck()
        {
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
                clearGrid();
            }

        }

       private void WordsList()
        {
            string line;
            string wordEntry = wordEntered.ToUpper();

            StreamReader sr = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\..\..\Resources\WordList.txt")); //Finding the path file starting from where the program is run
            line = sr.ReadLine();
            while(line != null)
            {
                line = sr.ReadLine();

                if (string.Equals(line, wordEntry, StringComparison.OrdinalIgnoreCase)) //Comparing strings without case sensitive
                {
                    DisplayAlert("yes", "OKAY", "Yes");
                    clearGrid();
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
            timer = new System.Timers.Timer(1000); //A timer that ticks every 
            timer.Elapsed += timeElapsed;
            timer.Start();
            timerLbl.Text = $"{totalTime}'s left";
        }


        private void timeElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Dispatch(() =>
            {
                    totalTime--;
                    timerLbl.Text = $"{totalTime}'s left";

                if(totalTime ==0)
                {
                    timer.Stop();
                    timerLbl.Text = $"Times up!";
                }
            }
            );
           
        }
        //MAKE 2 PLAYER MODE
    }
}
