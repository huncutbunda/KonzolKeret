class Program

{
    static int cursorX = 1;
    static int cursorY = 1;
    static char currentDrawingChar = '█';
    static ConsoleColor currentColor = ConsoleColor.White;

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
                    if (opciok[aktKivalasztas] == "Create")
                    {

                        StartDrawingTool();
                    }
                    else if (opciok[aktKivalasztas] == "Escape")
                    {
                        key = ConsoleKey.Escape;
                    }
                    enter = true;
                    break;
            }
        }
        while (key != ConsoleKey.Escape);
    }

    static void StartDrawingTool()
    {
        cursorX = 1;
        cursorY = 1;
        currentDrawingChar = '█';
        currentColor = ConsoleColor.White;

        DrawFrame();

        
        bool exitDrawing = false;


        while (!exitDrawing)
        {
            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(true);


                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (cursorY > 1) cursorY--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (cursorY < Console.WindowHeight - 2) cursorY++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (cursorX > 1) cursorX--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (cursorX < Console.WindowWidth - 2) cursorX++;
                        break;

                    case ConsoleKey.Escape:
                        exitDrawing = true;
                        break;


                    case ConsoleKey.D1:
                        currentDrawingChar = '█';
                        break;
                    case ConsoleKey.D2:
                        currentDrawingChar = '▓';
                        break;
                    case ConsoleKey.D3:
                        currentDrawingChar = '▒';
                        break;
                    case ConsoleKey.D4:
                        currentDrawingChar = '░';
                        break;


                    case ConsoleKey.D5:
                        currentColor = ConsoleColor.Red;
                        break;
                    case ConsoleKey.D6:
                        currentColor = ConsoleColor.Blue;
                        break;
                    case ConsoleKey.D7:
                        currentColor = ConsoleColor.Green;
                        break;
                    case ConsoleKey.D8:
                        currentColor = ConsoleColor.Yellow;
                        break;
                    case ConsoleKey.D9:
                        currentColor = ConsoleColor.Cyan;
                        break;
                }
            }
        }
    }
    static void DrawFrame()
    {
        Console.Clear();


        Console.SetCursorPosition(0, 0);
        Console.Write("╔");
        for (int i = 1; i < Console.WindowWidth - 1; i++)
        {
            Console.Write("═");
        }
        Console.Write("╗");


        for (int i = 1; i < Console.WindowHeight - 1; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("║");
            Console.SetCursorPosition(Console.WindowWidth - 1, i);
            Console.Write("║");
        }


        Console.SetCursorPosition(0, Console.WindowHeight - 1);
        Console.Write("╚");
        for (int i = 1; i < Console.WindowWidth - 1; i++)
        {
            Console.Write("═");
        }
        Console.Write("╝");

        Console.ResetColor();
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

