using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    public static class Utility
    {
        public static void DrawNight()
        {
            Console.Clear();
            var stars = new List<string>();
            stars.AddRange(new String[] {
                "        *       ",
                "              *       ",
                "    *            ",
                "          *          ",
                "  *        ",
                "           *    ",
                "        *      "
            });


            for (int i = 0; i < 200; i++)
            {

                Random s = new Random();
                int index = s.Next(stars.Count);

                if (i % 2 == 0)
                    Console.ForegroundColor = ConsoleColor.Gray;
                else if (i % 3 == 0)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                else
                    Console.ForegroundColor = ConsoleColor.DarkCyan;

                Console.Write(stars[index]);
                System.Threading.Thread.Sleep(2);

            }

            System.Threading.Thread.Sleep(700);
            Console.ForegroundColor = ConsoleColor.White;

        }
        public static void WriteName(string name)
        {
            Console.SetCursorPosition(7, 1);
            Console.WriteLine($"  ♥  ♥  ♥  {name}  ♥  ♥  ♥");
            Console.WriteLine();
        }

        public static void DrawState(Tama tama)
        {
            Console.SetCursorPosition(5, 17);

            Console.Write("Tired: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (var i = 0; i < tama.Tired / 10; i++)
            {
                Console.Write("♥");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"({tama.Tired}%) ");

            Console.Write("  Hungry: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (var i = 0; i < tama.Hunger / 10; i++)
            {
                Console.Write("♥");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"({tama.Hunger}%) ");

            Console.Write("  Happy: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            for (var i = 0; i < tama.Happy / 10; i++)
            {
                Console.Write("♥");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"({tama.Happy}%) ");

            Console.Write("  Full: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            for (var i = 0; i < tama.Full / 10; i++)
            {
                Console.Write("♥");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"({tama.Full}%) ");

            Console.WriteLine();
            Console.WriteLine();

        }

        public static void DrawPoop()
        {
            Console.Clear();
            Console.SetCursorPosition(5, 19);
            Console.Write("   Poop: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (var i = 0; i < 30; i++)
            {
                Console.Write("▲");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.WriteLine();

        }

        public static void WriteTamasState(Tama tama)
        {
            Console.Clear();
            tama.Full = GetSumValue(tama.Full, tama.FullnessRate, 1);
            tama.Hunger = GetSumValue(tama.Hunger, tama.HungerRate, 1);
            tama.Happy = GetSubValue(tama.Happy, tama.HappinessRate, 1);
            tama.Tired = GetSumValue(tama.Tired, tama.TirednessRate, 1);
            WriteName(tama.Name);
            DrawTama(tama.Stage);
            DrawState(tama);
        }

        public static void DrawTama(string stage)
        {
            Console.WriteLine($"       ♥  ♥  ♥  Stage : {stage}  ♥  ♥  ♥");
            ConsoleColor color;
            switch (stage)
            {
                case "baby":
                    color = ConsoleColor.Magenta;
                    Baby(color);
                    break;
                case "goodTeen":
                    color = ConsoleColor.Cyan;
                    GoodTeen(color);
                    break;
                case "badTeen":
                    color = ConsoleColor.Green;
                    BadTeen(color);
                    break;
                case "goodAdult":
                    color = ConsoleColor.Blue;
                    GoodAdult(color);
                    break;
                case "badAdult":
                    color = ConsoleColor.DarkGreen;
                    BadAdult(color);
                    break;
                case "angel":
                    color = ConsoleColor.DarkCyan;
                    Angel(color);
                    break;
                case "dead":
                    color = ConsoleColor.DarkGray;
                    Dead(color);
                    break;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static void TamaTalks(string msg, ConsoleColor color, string name)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.Write(" {0} > ", name);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(msg);
            Console.WriteLine();
            System.Threading.Thread.Sleep(1000);
        }

        public static void Hatching(string name, ConsoleColor color)
        {
            for (int i = 0; i < 15; i++)
            {
                Console.Clear();

                WriteName(name);

                if (i % 2 == 0)
                    Egg(color);
                else if (i % 3 == 0)
                    Egg2(color);
                else
                    Egg3(color);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(name + " is hatching!!");
                System.Threading.Thread.Sleep(200);
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Clear();
            WriteName(name);
            Egg(color);
            Console.ForegroundColor = ConsoleColor.White;
            System.Threading.Thread.Sleep(500);

            Console.Clear();
            WriteName(name);
            Egg4(color);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            TamaTalks("MOMMY!", color, name);
            System.Threading.Thread.Sleep(400);
            TamaTalks("*squeeek*", color, name);
            System.Threading.Thread.Sleep(800);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Egg(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("                    ■■       ");
            Console.WriteLine("                  ■    ■     ");
            Console.WriteLine("                ■        ■   ");
            Console.WriteLine("               ■          ■  ");
            Console.WriteLine("              ■            ■ ");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("               ■          ■  ");
            Console.WriteLine("                 ■ ■■■■ ■    ");
        }

        public static void Egg2(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("                   ■■        ");
            Console.WriteLine("                 ■    ■      ");
            Console.WriteLine("               ■        ■    ");
            Console.WriteLine("              ■          ■   ");
            Console.WriteLine("             ■             ■  ");
            Console.WriteLine("            ■               ■ ");
            Console.WriteLine("            ■               ■");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("               ■          ■  ");
            Console.WriteLine("                 ■ ■■■■ ■    ");
        }

        public static void Egg3(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("                     ■■      ");
            Console.WriteLine("                   ■    ■     ");
            Console.WriteLine("                 ■        ■   ");
            Console.WriteLine("               ■           ■  ");
            Console.WriteLine("              ■             ■ ");
            Console.WriteLine("             ■               ■");
            Console.WriteLine("             ■               ■");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("               ■          ■  ");
            Console.WriteLine("                 ■ ■■■■ ■    ");
        }

        public static void Egg4(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("                    ■■       ");
            Console.WriteLine("                  ■    ■     ");
            Console.WriteLine("                ■        ■   ");
            Console.WriteLine("              ■            ■ ");
            Console.WriteLine("             ■  ■ ■ ■■ ■ ■  ■");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("              ■  ■      ■  ■ ");
            Console.WriteLine("              ■    ■■■■    ■ ");
            Console.ForegroundColor = color;
            Console.WriteLine("             ■  ■ ■ ■■ ■ ■  ■");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("             ■              ■");
            Console.WriteLine("               ■          ■  ");
            Console.WriteLine("                 ■ ■■■■ ■    ");
        }


        public static void Baby(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                  ■ ■■■■ ■   ");
            Console.WriteLine("                ■          ■ ");
            Console.WriteLine("               ■  ■      ■  ■");
            Console.WriteLine("               ■    ■■■■    ■");
            Console.WriteLine("               ■            ■");
            Console.WriteLine("                ■          ■ ");
            Console.WriteLine("                  ■ ■■■■ ■   ");
        }

        public static void GoodTeen(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.WriteLine("                  ■ ■■■■ ■■   ");
            Console.WriteLine("                ■           ■  ");
            Console.WriteLine("               ■  ■       ■  ■ ");
            Console.WriteLine("               ■    ■■■■■    ■ ");
            Console.WriteLine("              ■■             ■■");
            Console.WriteLine("                ■           ■  ");
            Console.WriteLine("                 ■    ■    ■   ");
            Console.WriteLine("                  ■  ■ ■  ■   ");
            Console.WriteLine("                   ■     ■     ");
        }

        public static void BadTeen(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.WriteLine("                     ■ ■■■ ■      ");
            Console.WriteLine("                   ■         ■    ");
            Console.WriteLine("              ■■■■    ■   ■    ■  ");
            Console.WriteLine("             ■■■■■              ■ ");
            Console.WriteLine("                  ■            ■  ");
            Console.WriteLine("                  ■            ■  ");
            Console.WriteLine("                  ■             ■");
            Console.WriteLine("                  ■      ■       ■");
            Console.WriteLine("                    ■ ■ ■  ■ ■ ■  ");
        }

        public static void GoodAdult(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("                   ■       ■     ");
            Console.WriteLine("                  ■■■■   ■■■■    ");
            Console.WriteLine("                 ■■■■■■■■■■■■■   ");
            Console.WriteLine("                 ■           ■   ");
            Console.WriteLine("                ■  ■       ■  ■  ");
            Console.WriteLine("                ■     ■■■     ■  ");
            Console.WriteLine("              ■■■             ■■■");
            Console.WriteLine("                 ■           ■   ");
            Console.WriteLine("                  ■    ■    ■    ");
            Console.WriteLine("                   ■  ■ ■  ■     ");
            Console.WriteLine("                    ■     ■      ");
        }

        public static void BadAdult(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("                     ■ ■■■ ■      ");
            Console.WriteLine("                   ■         ■    ");
            Console.WriteLine("             ■■■■■    ■   ■    ■  ");
            Console.WriteLine("               ■               ■  ");
            Console.WriteLine("             ■■■■■              ■ ");
            Console.WriteLine("                  ■            ■  ");
            Console.WriteLine("                  ■            ■  ");
            Console.WriteLine("                  ■              ■");
            Console.WriteLine("                  ■      ■       ■");
            Console.WriteLine("                    ■   ■ ■   ■ ■ ");
            Console.WriteLine("                     ■      ■     ");
        }

        public static void Angel(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("                     ■■■■■       ");
            Console.WriteLine("                                 ");
            Console.WriteLine("                  ■■ ■ ■ ■ ■■    ");
            Console.WriteLine("                 ■           ■   ");
            Console.WriteLine("      ■         ■  ■       ■  ■  ");
            Console.WriteLine("    ■ ■ ■       ■     ■■■     ■  ");
            Console.WriteLine("      ■         ■             ■  ");
            Console.WriteLine("      ■          ■           ■   ");
            Console.WriteLine("                   ■       ■     ");
            Console.WriteLine("                      ■ ■        ");
            Console.WriteLine("                       ■         ");
        }

        public static void Dead(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("                   ■ ■ ■ ■      ");
            Console.WriteLine("                   ■     ■      ");
            Console.WriteLine("             ■ ■ ■ ■     ■ ■ ■ ■");
            Console.WriteLine("             ■      R.I.P.     ■");
            Console.WriteLine("             ■ ■ ■ ■     ■ ■ ■ ■");
            Console.WriteLine("                   ■     ■      ");
            Console.WriteLine("                   ■     ■      ");
            Console.WriteLine("                   ■     ■      ");
            Console.WriteLine("                   ■     ■      ");
            Console.WriteLine("                   ■     ■      ");
            Console.WriteLine("                   ■ ■ ■ ■      ");
        }


        public static void WriteMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void YouTalk(string owner)
        {
            Console.WriteLine();
            Console.Write(owner + "> ");
        }


        public static bool GetResponse(Tama tama, string you)
        {
            bool answer = false;
            bool final = false;

            WriteMsg("[ YES ]   [ NO ]");
            YouTalk(you);

            while (answer == false)
            {
                string readLine = Console.ReadLine()?.ToLower();

                if (readLine == "yes" || readLine == "y")
                {

                    answer = true;
                    final = true;
                }
                else if (readLine == "no" || readLine == "n")
                {
                    answer = true;
                }
                else
                {
                    WriteMsg("[ YES ]   [ NO ]");
                    YouTalk(you);
                }

            }
            return final;
        }

        public static int GetSumValue(int value, int rate, int factor)
        {
            if (value + (factor * rate) > 100)
                return 100;

            return value + (factor * rate);
        }

        public static int GetSubValue(int value, int rate, int factor)
        {
            if (value - (factor * rate) < 0)
                return 0;

            return value - (factor * rate);
        }

    }
}
