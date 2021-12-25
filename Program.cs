namespace tictactoe
{
        static class g
        {

                public static Dictionary<int,string> tiles = new Dictionary<int,string>();
                public static List<int[]> tilepatterns = new List<int[]>(){
                        new int[]{1,2,3},
                        new int[]{4,5,6},
                        new int[]{7,8,9},
                        new int[]{1,4,7},
                        new int[]{2,5,8},
                        new int[]{3,6,9},
                        new int[]{1,5,9},
                        new int[]{3,5,7},
                };
                public static string gameoverstate = "";
                public static int score = 0;
                public static int moves = 0;
                public static int totalmoves = 0;
                public static int rounds = 0;
        }
        public class main
        {
                private static void makegraphic()
                {
                        Console.Clear();
                        string text = "";
                        switch (g.gameoverstate)
                        {
                                case "results":
                                        text = @$"
| results |

score: {g.score.ToString()}
total moves: {g.totalmoves.ToString()}
rounds: {g.rounds.ToString()}

enter to continue playing, esc to exit
                                        ";
                                        break;
                                default:
                                        text = @$"
 {g.tiles[1]} | {g.tiles[2]} | {g.tiles[3]}
――― ――― ―――
 {g.tiles[4]} | {g.tiles[5]} | {g.tiles[6]}
――― ――― ―――
 {g.tiles[7]} | {g.tiles[8]} | {g.tiles[9]}
                                        ";
                                        if (g.gameoverstate != "")
                                                text += @$"

 {g.gameoverstate} | enter to restart, esc to show results
                                                ";
                                        break;
                        }
                        static void cfc(ConsoleColor cc)
                        {
                                Console.ForegroundColor = cc;
                        }
                        foreach (Char c in text.ToCharArray())
                        {
                                int intresult = -1;
                                try
                                {
                                        intresult = Int32.Parse(c.ToString());
                                }
                                catch{}
                                if (intresult != -1)
                                {
                                        cfc(ConsoleColor.DarkGray);
                                }
                                else if (c == 'O')
                                {
                                        cfc(ConsoleColor.Magenta);
                                }
                                else if (c == 'X')
                                {
                                        cfc(ConsoleColor.Cyan);
                                }
                                else
                                {
                                        cfc(ConsoleColor.Gray);
                                }
                                Console.Write(c);
                        }
                        Console.WriteLine();
                }
                private static void checkgameoverstate()
                {
                        if (g.gameoverstate == "results")
                                return;
                        string[] symbols = new string[]{"X","O"};
                        string symbolwin = "";
                        foreach (int[] tilepattern in g.tilepatterns)
                        {
                                foreach (string symbol in symbols)
                                {
                                        symbolwin = symbol;
                                        foreach (int tilepatternnum in tilepattern)
                                        {
                                                if (g.tiles[tilepatternnum] != symbol)
                                                {
                                                        symbolwin = "";
                                                        break;
                                                }
                                        }
                                        if (symbolwin != "")
                                                break;
                                }
                                if (symbolwin != "")
                                        break;
                        }
                        switch (symbolwin)
                        {
                                case "X":
                                        int addscore = Convert.ToInt32(5000/g.moves);
                                        g.score += addscore;
                                        g.gameoverstate = $"you win! (+{addscore.ToString()} score)";
                                        break;
                                case "O":
                                        g.score -= 1500;
                                        g.gameoverstate = "you lose.. (-1500 score)";
                                        break;
                                default:
                                        g.gameoverstate = "tie";
                                        var tilevalues = g.tiles.Values;
                                        foreach (string tilevalue in tilevalues)
                                        {
                                                if (tilevalue != "X" && tilevalue != "O")
                                                {
                                                        g.gameoverstate = "";
                                                        break;
                                                }
                                        }
                                        break;
                        }
                }
                private static void Main()
                {
                        Console.Title = "Tic Tac Toe";
                        Console.CursorVisible = false;
                        Console.SetWindowSize(65,15);
                        for (int num = 1;num <= 9;num++)
                        {
                                g.tiles.Add(num,num.ToString());
                        }
                        var random = new Random();
                        while (true)
                        {
                                Console.ResetColor();
                                checkgameoverstate();
                                makegraphic();
                                while (true)
                                {
                                        var keyinfo = Console.ReadKey(true);
                                        if (g.gameoverstate != "")
                                        {
                                                if (keyinfo.Key == ConsoleKey.Enter)
                                                {
                                                        foreach (int tile in g.tiles.Keys)
                                                                g.tiles[tile] = tile.ToString();
                                                        if (g.gameoverstate != "results")
                                                                g.rounds++;
                                                        g.moves = 0;
                                                        g.gameoverstate = "";
                                                        break;
                                                }
                                                if (keyinfo.Key == ConsoleKey.Escape)
                                                {
                                                        if (g.gameoverstate != "results")
                                                        {
                                                                g.rounds++;
                                                                g.gameoverstate = "results";
                                                        }
                                                        else
                                                        {
                                                                Environment.Exit(0);
                                                        }
                                                        break;
                                                }
                                        }
                                        if (g.gameoverstate != "")
                                                continue;
                                        try
                                        {
                                                int tile = Int32.Parse(keyinfo.KeyChar.ToString());
                                                if (g.tiles[tile] == "X" || g.tiles[tile] == "O" || tile == 0)
                                                        continue;
                                                g.tiles[tile] = "X";
                                                g.moves++;
                                                g.totalmoves++;
                                                checkgameoverstate();
                                                if (g.gameoverstate != "")
                                                        break;
                                                int randtile = 1;
                                                while (g.tiles[randtile] == "X" || g.tiles[randtile] == "O")
                                                {
                                                        randtile = random.Next(1,g.tiles.Count);
                                                }
                                                g.tiles[randtile] = "O";
                                                break;
                                        }
                                        catch{}
                                }
                        }
                }
        }
}