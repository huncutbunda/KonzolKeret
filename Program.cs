﻿class Program
{
    static void Main()
    {
        string[] opciok = { "Create", "Edit", "Delete", "Escape" };
        int aktKivalasztas = 0;
        ConsoleKey key;

        MenuRajzolas(opciok, aktKivalasztas, null);
        bool enter = false;
        do
        {
            if (!enter)
            {
                MenuRajzolas(opciok, aktKivalasztas, null);
            }
            else
            {
                MenuRajzolas(opciok, aktKivalasztas, opciok[aktKivalasztas]);
                enter = false;
            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (aktKivalasztas > 0)
                        aktKivalasztas--;
                    break;

                case ConsoleKey.DownArrow:
                    if (aktKivalasztas < opciok.Length - 1)
                        aktKivalasztas++;
                    break;
                case ConsoleKey.Enter:
                    MenuRajzolas(opciok, aktKivalasztas, opciok[aktKivalasztas]);
                    enter = true;
                    break;
            }
        }
        while (key != ConsoleKey.Escape);
    }

    static void MenuRajzolas(string[] opciok, int selectedOption, string kivalasztottSzoveg)
    {
        Console.Clear();

        int consoleWidth = Console.WindowWidth;
        int consoleHeight = Console.WindowHeight;
        int width = 20;

        int menuHeight = opciok.Length + 2;

        int felsoresz = (consoleHeight / 2) - (menuHeight / 2);
        int balresz = (consoleWidth / 2) - (width / 2);
        Console.SetCursorPosition(balresz, felsoresz - 1);
        Console.WriteLine("Nyomj ENTER-t a kiválasztáshoz");
        Console.SetCursorPosition(balresz, felsoresz);
        Console.WriteLine("+" + new string('-', width) + "+");

        for (int i = 0; i < opciok.Length; i++)
        {
            string padding = opciok[i].PadLeft((width / 2) + (opciok[i].Length / 2)).PadRight(width);

            Console.SetCursorPosition(balresz, felsoresz + 1 + i);


            if (i == selectedOption)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine("|" + padding + "|");
            Console.ResetColor();
        }

        Console.SetCursorPosition(balresz, felsoresz + menuHeight - 1);
        Console.WriteLine("+" + new string('-', width) + "+");

        if (!string.IsNullOrEmpty(kivalasztottSzoveg))
        {
            Console.SetCursorPosition(balresz, felsoresz + menuHeight + 1);
            Console.WriteLine("Kiválasztott opció: " + kivalasztottSzoveg);
        }


        }
    }

