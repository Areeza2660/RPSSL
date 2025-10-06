using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace RPSSL_GUII
{
    public partial class MainWindow : Window
    {
        private int playerScore = 0;
        private int agentScore = 0;
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();

            // Knyt knapper til eventhandlers
            BtnRock.Click += OnPlayerChoice;
            BtnPaper.Click += OnPlayerChoice;
            BtnScissors.Click += OnPlayerChoice;
            BtnLizard.Click += OnPlayerChoice;
            BtnSpock.Click += OnPlayerChoice;

            BtnReset.Click += (_, __) => ResetGame();
            BtnClose.Click += (_, __) => Close();
        }

        private void OnPlayerChoice(object? sender, RoutedEventArgs e)
        {
            if (sender is not Button button) return;

            string playerChoice = button.Content?.ToString() ?? "";
            string[] options = { "Rock", "Paper", "Scissors", "Lizard", "Spock" };
            string agentChoice = options[random.Next(options.Length)];

            ChoicesText.Text = $"Du: {playerChoice}   |   Agent: {agentChoice}";

            int result = Resolve(playerChoice, agentChoice);
            if (result == 1) playerScore++;
            else if (result == -1) agentScore++;

            ScoreText.Text = $"Stilling — Du: {playerScore} | Agent: {agentScore}";

            if (playerScore == 3 || agentScore == 3)
            {
                RoundText.Text = playerScore == 3 ? "🎉 Du vandt!" : "🤖 Agenten vandt!";
                DisableButtons();
            }
            else
            {
                RoundText.Text = "Vælg en ny figur…";
            }
        }

        private int Resolve(string player, string agent)
        {
            if (player == agent) return 0;

            return (player, agent) switch
            {
                ("Rock", "Scissors") or ("Rock", "Lizard") => 1,
                ("Paper", "Rock") or ("Paper", "Spock") => 1,
                ("Scissors", "Paper") or ("Scissors", "Lizard") => 1,
                ("Lizard", "Spock") or ("Lizard", "Paper") => 1,
                ("Spock", "Rock") or ("Spock", "Scissors") => 1,
                _ => -1,
            };
        }

        private void DisableButtons()
        {
            BtnRock.IsEnabled = false;
            BtnPaper.IsEnabled = false;
            BtnScissors.IsEnabled = false;
            BtnLizard.IsEnabled = false;
            BtnSpock.IsEnabled = false;
        }

        private void ResetGame()
        {
            playerScore = 0;
            agentScore = 0;

            ScoreText.Text = "Stilling — Du: 0 | Agent: 0";
            RoundText.Text = "Vælg en figur for at starte spillet…";
            ChoicesText.Text = "Du: -   |   Agent: -";

            BtnRock.IsEnabled = true;
            BtnPaper.IsEnabled = true;
            BtnScissors.IsEnabled = true;
            BtnLizard.IsEnabled = true;
            BtnSpock.IsEnabled = true;
        }
    }
}
