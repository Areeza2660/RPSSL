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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnShapeClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && Enum.TryParse<Shape>(btn.Content.ToString(), out Shape human))
            {
                Shape agent = (Shape)_random.Next(0, 5);

                TxtAgentChoice!.Text = $"Agent chose: {agent}";
                TxtResult.Text = $"Result: {DetermineResult(human, agent)}";
            }
        }

        private string DetermineResult(Shape human, Shape agent)
        {
            int diff = (int)agent - (int)human;

            return diff switch
            {
                0 => "Tie",
                -4 or -2 or 1 or 3 => "You win!",
                -3 or -1 or 2 or 4 => "You lose!",
                _ => "Error"
            };
        }
    }
}

/*
 Jeg har lavet mine koder ved hjælp af GEKOS hjemmeside
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
