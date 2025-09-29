using System;

enum Shape { Rock = 1, Paper, Scissors, Spock, Lizard }

static class Game
{
    // return: 1 = player vinder, -1 = computer vinder, 0 = uafgjort
    public static int Resolve(Shape p, Shape c)
    {
        if (p == c) return 0;
        return (p, c) switch
        {
            (Shape.Rock,     Shape.Scissors) or (Shape.Rock,     Shape.Lizard)   => 1,
            (Shape.Paper,    Shape.Rock)     or (Shape.Paper,    Shape.Spock)    => 1,
            (Shape.Scissors, Shape.Paper)    or (Shape.Scissors, Shape.Lizard)   => 1,
            (Shape.Lizard,   Shape.Spock)    or (Shape.Lizard,   Shape.Paper)    => 1,
            (Shape.Spock,    Shape.Scissors) or (Shape.Spock,    Shape.Rock)     => 1,
            _ => -1
        };
    }

    public static string Name(Shape s) => s.ToString();
}

class Program
{
    static readonly Random Rnd = new();

    static Shape AskPlayer()
    {
        while (true)
        {
            Console.Write("Vælg 1=Rock, 2=Paper, 3=Scissors, 4=Spock, 5=Lizard: ");
            if (int.TryParse(Console.ReadLine(), out int n) && n is >= 1 and <= 5)
                return (Shape)n;
            Console.WriteLine("Ugyldigt valg – prøv igen.");
        }
    }

    static Shape RandomShape() => (Shape)Rnd.Next(1, 6);

    static void Main()
    {
        int player = 0, computer = 0;
        const int WinningScore = 3;

        Console.WriteLine("Rock Paper Scissors Spock Lizard — først til 3 vinder.");
        while (player < WinningScore && computer < WinningScore)
        {
            Shape p = AskPlayer();
            Shape c = RandomShape();

            int r = Game.Resolve(p, c);
            if (r == 1) { player++;  Console.WriteLine($"Du: {Game.Name(p)}  |  PC: {Game.Name(c)}  → Du vandt runden"); }
            else if (r == -1) { computer++; Console.WriteLine($"Du: {Game.Name(p)}  |  PC: {Game.Name(c)}  → PC vandt runden"); }
            else Console.WriteLine($"Du: {Game.Name(p)}  |  PC: {Game.Name(c)}  → Uafgjort");

            Console.WriteLine($"Stilling: Dig {player} – {computer} PC\n");
        }

        Console.WriteLine(player == WinningScore ? "*** DU VANDT ***" : "*** PC VANDT ***");
    }
}
