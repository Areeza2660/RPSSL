using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace RPSSL
{
    public partial class MainWindow : Window
    {
        private Random _random = new Random();

        private enum Shape
        {
            Rock,
            Paper,
            Scissors,
            Lizard,
            Spock
        }

        private enum RoundResult
        {
            Tie,
            HumanWin,
            AgentWin
        }

        private int _humanScore = 0;
        private int _agentScore = 0;
        private bool _gameOver = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnShapeClick(object sender, RoutedEventArgs e)
        {
            if (_gameOver)
                return;

            if (sender is Button btn && Enum.TryParse<Shape>(btn.Content.ToString(), out Shape human))
            {
                Shape agent = (Shape)_random.Next(0, 5);

                TxtAgentChoice!.Text = $"Agent chose: {agent}";

                RoundResult roundResult = DetermineResult(human, agent);

                switch (roundResult)
                {
                    case RoundResult.Tie:
                        TxtResult.Text = "Result: Tie";
                        break;

                    case RoundResult.HumanWin:
                        _humanScore++;
                        TxtResult.Text = "Result: You win!";
                        break;

                    case RoundResult.AgentWin:
                        _agentScore++;
                        TxtResult.Text = "Result: You lose!";
                        break;
                }

                // Opdater score
                TxtScore.Text = $"Score – You: {_humanScore} | Agent: {_agentScore}";

                // Tjek om nogen har nået 3 point
                CheckForGameOver();
            }
        }

        private RoundResult DetermineResult(Shape human, Shape agent)
        {
            int diff = (int)agent - (int)human;

            return diff switch
            {
                0 => RoundResult.Tie,
                -4 or -2 or 1 or 3 => RoundResult.HumanWin,
                -3 or -1 or 2 or 4 => RoundResult.AgentWin,
                _ => RoundResult.Tie // fallback
            };
        }

        private void CheckForGameOver()
        {
            if (_humanScore >= 3)
            {
                TxtResult.Text = "Game over: You reached 3 points. You win";
                EndGame();
            }
            else if (_agentScore >= 3)
            {
                TxtResult.Text = "Game over: Agent reached 3 points. You loose";
                EndGame();
            }
        }

        private void EndGame()
        {
            _gameOver = true;
            
            var panel = this.FindControl<WrapPanel>("ButtonPanel");
            foreach (var child in panel.Children)
            {
                if (child is Button b)
                    b.IsEnabled = false;
            }
        }
    }
}
/*
 Jeg har lavet mine koder ved hjælp af GEKOS 
Kildekommentar – brugt undervisningsmateriale:

- Random (_random.Next())
  → Fra "Generating random numbers"
    https://industrial-programming.aydos.de/sections/generating-random-numbers.html

- Enum Shape (Rock, Paper, Scissors, Lizard, Spock)
  → Fra Activity 29: RPSSL resolution logic
    https://industrial-programming.aydos.de/sections/rpssl-resolution.html

- Click event handler (OnShapeClick)
  → Fra Activity 32: Currency converter GUI
    https://industrial-programming.aydos.de/sections/currency-converter-gui.html

- Enum.TryParse(...) til at konvertere knaptekst til enum
  → Fra eksempler på "Converting input" + "Enum basics" i enum-sektionen

- Switch expression i DetermineResult()
  → Fra "Switch" sektionen
    https://industrial-programming.aydos.de/sections/switch.html

- RPSSL-vind/regler (diff tabel)
  → Direkte baseret på lærens resolution table i Activity 29.
*/
