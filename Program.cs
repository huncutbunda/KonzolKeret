class Program
{
    static int cursorX = 1;
    static int cursorY = 1;
    static char currentDrawingChar = '█';
    static ConsoleColor currentColor = ConsoleColor.White;
    static char[,] drawingGrid;
    static int consoleWidth;
    static int consoleHeight;


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
                    aktKivalasztas--;
                    if (aktKivalasztas < 0)
                    {
                        aktKivalasztas = opciok.Length - 1; 
                    }
                    break;

                case ConsoleKey.DownArrow:
                    aktKivalasztas++;
                    if (aktKivalasztas >= opciok.Length)
                    {
                        aktKivalasztas = 0; 
                    }
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
            MenuRajzolas(opciok, aktKivalasztas, null);
        }
        while (key != ConsoleKey.Escape);
    }
    static void MenuRajzolas(string[] opciok, int aktKivalasztas, string kivalasztottSzoveg)
    {
        Console.Clear();

        int consoleWidth = Console.WindowWidth;
        int consoleHeight = Console.WindowHeight;
        int width = 20;
        int menuHeight = opciok.Length + 2;
        int felsoresz = (consoleHeight / 2) - (menuHeight / 2);
        int balresz = (consoleWidth / 2) - (width / 2);
        Console.SetCursorPosition(balresz, felsoresz - 1);
        Console.WriteLine("Menü (ENTER)");
        Console.SetCursorPosition(balresz, felsoresz);
        Console.WriteLine("+" + new string('-', width) + "+");

        for (int i = 0; i < opciok.Length; i++)
        {
            string padding = opciok[i].PadLeft((width / 2) + (opciok[i].Length / 2)).PadRight(width);

            Console.SetCursorPosition(balresz, felsoresz + 1 + i);


            if (i == aktKivalasztas)
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


    static void StartDrawingTool()
    {
        consoleWidth = Console.WindowWidth;
        consoleHeight = Console.WindowHeight;

        drawingGrid = new char[consoleWidth, consoleHeight];
        for (int i = 0; i < consoleWidth; i++)
        {
            for (int j = 0; j < consoleHeight; j++)
            {
                drawingGrid[i, j] = ' '; 
            }
        }

        cursorX = 1;
        cursorY = 1;
        currentDrawingChar = '█';
        currentColor = ConsoleColor.White;

        DrawFrame();

        bool isSpacebarPressed = false;
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

                    case ConsoleKey.Spacebar:
                        isSpacebarPressed = true;
                        break;

                    case ConsoleKey.S:
                        SaveDrawingToFile(); 
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
            if (isSpacebarPressed)
            {
                ColorCurrentPosition(currentDrawingChar, currentColor);
                if (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Spacebar)
                {
                    isSpacebarPressed = false;
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
    static void ColorCurrentPosition(char drawChar, ConsoleColor color)
    {
        Console.SetCursorPosition(cursorX, cursorY);
        Console.ForegroundColor = color;
        Console.Write(drawChar);
        Console.ResetColor();
    }
    static void SaveDrawingToFile()
    {
        
        Console.SetCursorPosition(0, Console.WindowHeight - 2);
        Console.Write("Enter the file name to save: ");
        string fileName = Console.ReadLine();
        string filePath = fileName + ".txt";

        
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int y = 1; y < Console.WindowHeight - 1; y++) 
            {
                for (int x = 1; x < Console.WindowWidth - 1; x++) 
                {
                    writer.Write(drawingGrid[x, y]);
                }
                writer.WriteLine(); 
            }
        }

        
        Console.SetCursorPosition(0, Console.WindowHeight - 1);
        Console.WriteLine($"Rajz így lett mentve: {filePath}");
    }
}


    


