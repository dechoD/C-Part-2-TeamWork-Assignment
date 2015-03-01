﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;



class CerberusMinesweeper
{
    /// <summary>
    /// Prints a message on a given position
    /// </summary>
    /// <param name="message"> the message to be printed </param>
    /// <param name="xCoord"> horizontal offset in symbols from left to right </param>
    /// <param name="yCoord"> vertical offset in symbols from top to bottom </param>
    static void PrintMessageOnConsole(string message, int xCoord, int yCoord)
    {
        Console.SetCursorPosition(yCoord, xCoord);
        Console.WriteLine(message);
    }

    /// <summary>
    /// Method used to print a difficulty menu on the console
    /// </summary>
    /// <param name="xCord"> horizontal of the upper left corner of the menu </param>
    /// <param name="yCoord"> vertical position of the upper left corner of the menu </param>
    /// <returns> An integer representing the difficulty </returns>    
    /// 

    public struct MCursor          // ------- Yankov-------
    {
        public int x;
        public int y;
        public string text;
        public ConsoleColor color;

    }
    static void PrintPosition(int x, int y, string c, ConsoleColor color = ConsoleColor.White)  // ---- Yankov-------
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(c);
    }
    static int PrintDifficultyMenu()   //----- Yankov ----   //int xCord, int yCoord
    {
        Console.CursorVisible = false;
        MCursor objCursor = new MCursor();
        objCursor.x = 3;
        objCursor.y = 3;
        objCursor.color = ConsoleColor.Green;
        objCursor.text = ">";


        // Welcome to Minesweeper
        PrintPosition(Console.WindowWidth / 2 - 11, 0, " Welcome to Minesweeper ", ConsoleColor.White);
        PrintPosition(5, 3, "Easy (9x9)", ConsoleColor.White);
        PrintPosition(5, 5, "Medium (16x16)", ConsoleColor.White);
        PrintPosition(5, 7, "Hard (16x30)", ConsoleColor.White);
        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedkey = Console.ReadKey(true);
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);

                }
                if (objCursor.y == 3)  // Option easy
                {
                    if (pressedkey.Key == ConsoleKey.DownArrow)
                    {
                        objCursor.y = 5;
                        PrintPosition(3, 3, " ", ConsoleColor.White);
                    }
                    else if (pressedkey.Key == ConsoleKey.UpArrow)
                    {
                        objCursor.y = 7;
                        PrintPosition(3, 3, " ", ConsoleColor.White);

                    }
                }
                else if (objCursor.y == 5) // Option medium
                {
                    if (pressedkey.Key == ConsoleKey.DownArrow)
                    {
                        objCursor.y = 7;
                        PrintPosition(3, 5, " ", ConsoleColor.White);
                    }
                    else if (pressedkey.Key == ConsoleKey.UpArrow)
                    {
                        objCursor.y = 3;
                        PrintPosition(3, 5, " ", ConsoleColor.White);
                    }
                }
                else if (objCursor.y == 7) // Option Hard
                {
                    if (pressedkey.Key == ConsoleKey.DownArrow)
                    {
                        objCursor.y = 3;
                        PrintPosition(3, 7, " ", ConsoleColor.White);
                    }
                    else if (pressedkey.Key == ConsoleKey.UpArrow)
                    {
                        objCursor.y = 5;
                        PrintPosition(3, 7, " ", ConsoleColor.White);
                    }
                }
                if (pressedkey.Key == ConsoleKey.Enter && objCursor.y == 3)
                {
                    return 1;
                }
                else if (pressedkey.Key == ConsoleKey.Enter && objCursor.y == 5)
                {
                    return 2;
                }
                else if (pressedkey.Key == ConsoleKey.Enter && objCursor.y == 7)
                {
                    return 3;
                }

            }
            PrintPosition(objCursor.x, objCursor.y, objCursor.text, objCursor.color);

        }
    }

    /// <summary>
    /// Method to count the mines left to be marked. That will count the flagged cells.
    /// flagged mines are marked "2" <-- note its a string
    /// </summary>
    /// <param name="board"> The board with the mines and cell values </param>
    /// <param name="visibilityArray"> The array with the visibility , flagged mines are "2" </param>
    /// <returns> the count to be printed by PrintMinesLeft method </returns>
    static int CountMinesLeft(string[,] board, string[,] visibilityArray, int count)
    {
        //Niko worked here
        // TODO: Implement a method to calculate the mines left to be marked
        if (visibilityArray.Length == 81)
        {
            count = 10;
        }
        else if (visibilityArray.Length == 256)
        {
            count = 40;
        }
        else if (visibilityArray.Length == 480)
        {
            count = 99;
        }

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int p = 0; p < board.GetLength(1); p++)
            {

                if (visibilityArray[i, p] == "2")
                {
                    count--;
                }

            }
        }
        return count;
    }

    /// <summary>
    /// Prints a mines left count
    /// </summary>
    /// <param name="array"> The array with the mines and numbers </param>
    static void PrintMinesLeft(string[,] board, string[,] visibilityArray)
    {
        //Niko worked here
        // TODO: Implement method to print the mines left somewhere using PrintMessageOnConsole Method
        Console.SetCursorPosition(7, 0);
        Console.WriteLine("   ||  Mines left to be marked: {0}", CountMinesLeft(board, visibilityArray, 0) + " ");
    }

    /// <summary>
    /// Keeping count of the seconds after the game has started    
    /// </summary>
    /// <param name="start"> the time the game started </param>
    public static void PrintTimeElapsed(DateTime start)
    {
        //Pavleta worked here...         
        Console.SetCursorPosition(1, 0);
        TimeSpan difference = DateTime.Now - start;
        int seconds = (int)difference.TotalSeconds;
        Console.Write("{0} sec", seconds);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <returns></returns>
    public static int EndTimeElapsed(DateTime start, DateTime end)
    {
        //Vely worked here...   (for points saving)      
        TimeSpan difference = end - start;
        int seconds = (int)difference.TotalSeconds;
        return seconds;

    }
    /// <summary>
    /// Method used from OpenZeroesArea method to reveal all eight cells arround a cell
    /// </summary>
    /// <param name="visibility"> array used by print board method </param>
    /// <param name="row"> row of the cell to be revealed arround </param>
    /// <param name="col"> column of the cell to be revealed arround </param>
    static void RevealArroundZero(string[,] visibility, int row, int col)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                try
                {
                    visibility[row + i, col + j] = "1";
                }
                catch (IndexOutOfRangeException)
                {
                }
            }
        }
    }

    /// <summary>
    /// Recursive method that opens the area with zeroes and the attached cells
    /// </summary>
    /// <param name="board"> play board </param>
    /// <param name="visibilityArray"> array used by print board method </param>
    /// <param name="row"> current row </param>
    /// <param name="col"> current column </param>
    /// <param name="visited"> temporary array used to avoid infinite recursion </param>
    static void OpenZeroesArea(string[,] board, string[,] visibilityArray, int row, int col, bool[,] visited)
    {
        int maxRow = board.GetUpperBound(0);
        int maxCol = board.GetUpperBound(1);
        RevealArroundZero(visibilityArray, row, col);
        visited[row, col] = true;

        if (row - 1 >= 0)
        {
            if (board[row - 1, col] == "0" && !visited[row - 1, col])
            {
                OpenZeroesArea(board, visibilityArray, row - 1, col, visited);
            }
        }

        if (row + 1 <= maxRow)
        {
            if (board[row + 1, col] == "0" && !visited[row + 1, col])
            {
                OpenZeroesArea(board, visibilityArray, row + 1, col, visited);
            }
        }

        if (col + 1 <= maxCol)
        {
            if (board[row, col + 1] == "0" && !visited[row, col + 1])
            {
                OpenZeroesArea(board, visibilityArray, row, col + 1, visited);
            }
        }

        if (col - 1 >= 0)
        {
            if (board[row, col - 1] == "0" && !visited[row, col - 1])
            {
                OpenZeroesArea(board, visibilityArray, row, col - 1, visited);
            }
        }

        if (col - 1 >= 0 && row - 1 >= 0)
        {
            if (board[row - 1, col - 1] == "0" && !visited[row - 1, col - 1])
            {
                OpenZeroesArea(board, visibilityArray, row - 1, col - 1, visited);
            }
        }

        if (col + 1 <= maxCol && row + 1 <= maxRow)
        {
            if (board[row + 1, col + 1] == "0" && !visited[row + 1, col + 1])
            {
                OpenZeroesArea(board, visibilityArray, row + 1, col + 1, visited);
            }
        }

        if (col - 1 >= 0 && row + 1 <= maxRow)
        {
            if (board[row + 1, col - 1] == "0" && !visited[row + 1, col - 1])
            {
                OpenZeroesArea(board, visibilityArray, row + 1, col - 1, visited);
            }
        }

        if (col + 1 <= maxCol && row - 1 >= 0)
        {
            if (board[row - 1, col + 1] == "0" && !visited[row - 1, col + 1])
            {
                OpenZeroesArea(board, visibilityArray, row - 1, col + 1, visited);
            }
        }

        return;
    }

    /// <summary>
    /// Helper method used by PrintBoard returning the color of the digit revealed
    /// </summary>
    /// <param name="minesArray"> play board </param>
    /// <param name="curRow"> row of the number </param>
    /// <param name="curCol"> column of the number </param>
    /// <returns> color in which the number will be printed </returns>
    static ConsoleColor GetDigitColor(string[,] minesArray, int curRow, int curCol)
    {
        int digit = int.Parse(minesArray[curRow, curCol]);

        switch (digit)
        {
            case 1:
                return ConsoleColor.Blue;
            case 2:
                return ConsoleColor.DarkGreen;
            case 3:
                return ConsoleColor.Red;
            case 4:
                return ConsoleColor.DarkBlue;
            case 5:
                return ConsoleColor.DarkRed;
            case 6:
                return ConsoleColor.DarkCyan;
            case 7:
                return ConsoleColor.Black;
            case 8:
                return ConsoleColor.DarkGray;
            default:
                return ConsoleColor.Blue;
        }
    }

    /// <summary>
    /// Draws the board
    /// </summary>
    /// <param name="minesArray"> the array with the mines and numbers</param>
    /// <param name="visibilityArray">
    /// The array with the information about what is visible to the player
    /// 0 - invisible
    /// 1 - visible
    /// 2 - marked as mine
    /// </param>

    static void PrintBoard(string[,] minesArray, string[,] visibilityArray, int curRow, int curCol)
    {
        Console.SetCursorPosition(0, 1);


        for (int row = 0; row < minesArray.GetLength(0); row++)
        {
            for (int col = 0; col < minesArray.GetLength(1); col++)
            {
                // If its the cursor
                if (row == curRow && col == curCol)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;

                    if (visibilityArray[row, col] == "0" || visibilityArray[row, col] == "2")
                    {
                        Console.Write(" ");
                        Console.ResetColor();
                        continue;
                    }
                    else
                    {
                        if (minesArray[row, col] == "0")
                        {
                            Console.Write(" ");
                            Console.ResetColor();
                            continue;
                        }
                        else
                        {
                            Console.Write(minesArray[row, col]);
                            Console.ResetColor();
                            continue;
                        }
                    }
                }

                // If marked as mine
                if (visibilityArray[row, col] == "2")
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write((char)16);
                    Console.ResetColor();
                    continue;
                }

                // If visible
                if (visibilityArray[row, col] == "1")
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = GetDigitColor(minesArray, row, col);

                    if (minesArray[row, col] != "0")
                    {
                        Console.Write(minesArray[row, col]);
                        Console.ResetColor();
                        continue;
                    }
                    else
                    {
                        Console.Write(" ");
                        Console.ResetColor();
                        continue;
                    }

                }

                // If invisible
                else if (visibilityArray[row, col] == "0")
                {
                    if (row % 2 == 0)
                    {
                        if (col % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write(" ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.Write(" ");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        if (col % 2 == 0)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.Write(" ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write(" ");
                            Console.ResetColor();
                        }
                    }
                }
            }

            Console.WriteLine();
        }
    }

    /// <summary>
    /// Creates a playing field with the given dimensions
    /// </summary>
    /// <param name="difficulty"> The difficulty determines the row and column count </param>
    /// <returns> An integer array with the given dimensions </returns>
    static string[,] CreateBoard(int difficulty)
    {
        // TODO: implement custom sizes if we have time

        string[,] board;

        if (difficulty == 1)
        {
            board = new string[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = "0";
                }
            }
        }
        else if (difficulty == 2)
        {
            board = new string[16, 16];

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    board[i, j] = "0";
                }
            }
        }
        else
        {
            board = new string[16, 30];

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    board[i, j] = "0";
                }
            }
        }

        return board;
    }

    /// <summary>
    /// Method for visualizing the board for debugging purposes only
    /// </summary>
    /// <param name="array"> the array needs debugging </param>
    static void DebugPrintBoard(string[,] array)
    {
        for (int row = 0; row < array.GetLength(0); row++)
        {
            for (int col = 0; col < array.GetLength(1); col++)
            {
                Console.Write("{0,2}", array[row, col]);
            }

            Console.WriteLine();
        }
    }

    /// <summary>
    /// Populates an array with mines symbol "*" at randomly generated positions
    /// </summary>
    /// <param name="array"> mines and numbers array </param>
    /// <param name="difficulty"> determines the number of mines to be generated </param>
    static void FillWithRandomMines(string[,] array, int difficulty)
    {
        int mines = 0;
        int maxRow = int.MinValue;
        int maxCol = int.MinValue;
        int placedMines = 0;

        if (difficulty == 1)
        {
            mines = 10;
            maxRow = 9;
            maxCol = 9;
        }
        else if (difficulty == 2)
        {
            mines = 40;
            maxRow = 16;
            maxCol = 16;
        }
        else if (difficulty == 3)
        {
            mines = 99;
            maxRow = 16;
            maxCol = 30;
        }

        Random randGenerator = new Random();

        while (placedMines < mines)
        {
            int row = randGenerator.Next(0, maxRow);
            int col = randGenerator.Next(0, maxCol);

            if (array[row, col] == "0")
            {
                array[row, col] = "*";
                placedMines++;
            }
        }
    }

    /// <summary>
    /// Checks the cells around the one defined by row and col for mines
    /// </summary>
    /// <param name="row"> row of the cell we need </param>
    /// <param name="col"> column of the cell we need </param>
    /// <param name="array"> the mine and numbers array </param>
    /// <returns> the number of mines in the 8 neighbouring cells </returns>
    static int GetCountOfMinesAround(int row, int col, string[,] array)
    {
        int count = 0;

        if (array[row, col] == "*")
        {
            throw new ArgumentException("Trying to check a mine cell!");
        }

        for (int currRow = -1; currRow <= 1; currRow++)
        {
            for (int currCol = -1; currCol <= 1; currCol++)
            {
                try
                {
                    if (array[row + currRow, col + currCol] == "*")
                    {
                        count++;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                }
            }
        }

        return count;
    }

    /// <summary>
    /// Fills the cells around the mines with the right numbers
    /// </summary>
    /// <param name="array"> the array with the mines and the numbers </param>
    static void CalculateDigitsArroundMines(string[,] array)
    {
        for (int row = 0; row < array.GetLength(0); row++)
        {
            for (int col = 0; col < array.GetLength(1); col++)
            {
                if (array[row, col] != "*")
                {
                    array[row, col] = Convert.ToString(GetCountOfMinesAround(row, col, array));
                }
            }
        }
    }

    /// <summary>
    /// Write end game message with high score alert if any
    /// </summary>
    /// <param name="scoreFilePath"> path to the file with the high scores </param>
    static int CheckIfGameWon(string[,] minesArray, string[,] visibilityArray, int counter)
    {
        //Niko worked here
        //DONE: Made the counter add one for every visible block
        for (int i = 0; i < minesArray.GetLength(0); i++)
        {
            for (int p = 0; p < minesArray.GetLength(1); p++)
            {
                if (visibilityArray[i, p] == "1")
                {
                    counter++;
                }
            }
        }
        return counter;
    }

    static void WriteEndGameMessage()
    {
        // TODO: Implement message print for end of the game (win and lose)
        Console.Clear();
        //Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WindowWidth = 135;
        Console.WriteLine(@"End Game

───────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
─██████████████─██████████████─████████████████───██████████████───██████████████─████████████████───██████──██████─██████████████─
─██░░░░░░░░░░██─██░░░░░░░░░░██─██░░░░░░░░░░░░██───██░░░░░░░░░░██───██░░░░░░░░░░██─██░░░░░░░░░░░░██───██░░██──██░░██─██░░░░░░░░░░██─
─██░░██████████─██░░██████████─██░░████████░░██───██░░██████░░██───██░░██████████─██░░████████░░██───██░░██──██░░██─██░░██████████─
─██░░██─────────██░░██─────────██░░██────██░░██───██░░██──██░░██───██░░██─────────██░░██────██░░██───██░░██──██░░██─██░░██─────────
─██░░██─────────██░░██████████─██░░████████░░██───██░░██████░░████─██░░██████████─██░░████████░░██───██░░██──██░░██─██░░██████████─
─██░░██─────────██░░░░░░░░░░██─██░░░░░░░░░░░░██───██░░░░░░░░░░░░██─██░░░░░░░░░░██─██░░░░░░░░░░░░██───██░░██──██░░██─██░░░░░░░░░░██─
─██░░██─────────██░░██████████─██░░██████░░████───██░░████████░░██─██░░██████████─██░░██████░░████───██░░██──██░░██─██████████░░██─
─██░░██─────────██░░██─────────██░░██──██░░██─────██░░██────██░░██─██░░██─────────██░░██──██░░██─────██░░██──██░░██─────────██░░██─
─██░░██████████─██░░██████████─██░░██──██░░██████─██░░████████░░██─██░░██████████─██░░██──██░░██████─██░░██████░░██─██████████░░██─
─██░░░░░░░░░░██─██░░░░░░░░░░██─██░░██──██░░░░░░██─██░░░░░░░░░░░░██─██░░░░░░░░░░██─██░░██──██░░░░░░██─██░░░░░░░░░░██─██░░░░░░░░░░██─
─██████████████─██████████████─██████──██████████─████████████████─██████████████─██████──██████████─██████████████─██████████████─
───────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────");
        Console.ForegroundColor = ConsoleColor.White;


    }

    /// <summary>
    /// Write high scores after the game has finished
    /// </summary>
    /// <param name="scoreFilePath"> path to the file keeping the high scores </param>

    public static void WriteHighScore(int currentHighScore, int currentScore, int dificulty)
    {
        ///Vely worked here
        // TODO: Implement high scores print on the console with current highscore in different color

        Console.Write("Please enter your name for the top scoreboard: ");
        string name = Console.ReadLine();

        StreamReader readerToAr = new StreamReader("..\\..\\Highscores.txt");
        string[] lines = readerToAr.ReadToEnd().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        readerToAr.Close();

        string dificultyStr = "";
        if (dificulty == 1)
        {
            dificultyStr = "easy";
        }
        else if (dificulty == 2)
        {
            dificultyStr = "medium";
        }
        else if (dificulty == 3)
        {
            dificultyStr = "hard";
        }

        if (currentScore < currentHighScore)
        {
            string writeHighScore = String.Format("user: {0} dificulty: {1} score: {2}", name, dificultyStr, currentScore);
            lines[dificulty - 1] = writeHighScore;
            File.Delete("..\\..\\Highscores.txt");
            StreamWriter writer = new StreamWriter("..\\..\\Highscores.txt", true);
            using (writer)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    writer.WriteLine(lines[i]);
                }

            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You have the highest score!\n\rYour score is: {0}", currentScore);
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Current highscore is: {0}\n\rYour score is: {1}", currentHighScore, currentScore);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    /// <summary>
    /// Ask the user if he wants new game
    /// </summary>
    static void AskForNewGame(bool win)
    {
        // Implement logic for new game question to user and exit if negative
        // Ivan worked here
        if (!win)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("***GAME OVER***");
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine();
        Console.WriteLine("Press 1 for new game");
        Console.WriteLine("Press 2 for exit");
        string choise = Console.ReadLine();

        bool check = false;
        do
        {

            switch (choise)
            {
                case "1":
                    {
                        check = true;
                        Console.Clear();
                        Main();
                        // returns to the main method if the user choose new game
                        break;
                    }
                case "2":
                    {
                        check = true;
                        break;
                    }
                default:
                    Console.Write("Please press 1 for new game or 2 for exit: ");
                    choise = Console.ReadLine(); ///if we try with something other than digit it will boom
                    check = false;
                    break;
            }
        } while (!check);


    }

    static void Main()
    {

        //while (gameInProgress)
        //{
        //// New game start
        //bool gameInProgress = true;

        //// Difficulty menu
        //Console.BufferHeight = Console.WindowHeight = 30; // -- To diable vertical scrollbar  
        //Console.BufferWidth = Console.WindowWidth = 70;   // To disable horizontal scrollbar . Max value is your screen resolution , so keep in mind.
        Console.Title = "Misesweeper";  // Title of console's window
        int dificulty = PrintDifficultyMenu();
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;

        string[,] board = CreateBoard(dificulty);
        string[,] visibilityBoard = CreateBoard(dificulty);
        FillWithRandomMines(board, dificulty);
        CalculateDigitsArroundMines(board);
        int cursorRow = board.GetLength(0) / 2;
        int cursorCol = board.GetLength(1) / 2;
        //Console.WriteLine("Random board:");
        //DebugPrintBoard(board);
        DateTime start = DateTime.Now;

        bool win = false;

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo userInput = Console.ReadKey(true);
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                if (userInput.Key == ConsoleKey.LeftArrow)
                {
                    if (cursorCol > 0) cursorCol -= 1;
                }

                if (userInput.Key == ConsoleKey.RightArrow)
                {
                    if (cursorCol < board.GetUpperBound(1)) cursorCol += 1;
                }

                if (userInput.Key == ConsoleKey.UpArrow)
                {
                    if (cursorRow > 0) cursorRow -= 1;
                }

                if (userInput.Key == ConsoleKey.DownArrow)
                {
                    if (cursorRow < board.GetUpperBound(0)) cursorRow += 1;
                }

                if (userInput.Key == ConsoleKey.A)
                {
                    if (visibilityBoard[cursorRow, cursorCol] == "0")
                    {
                        visibilityBoard[cursorRow, cursorCol] = "2";
                    }
                    else if (visibilityBoard[cursorRow, cursorCol] == "2")
                    {
                        visibilityBoard[cursorRow, cursorCol] = "0";
                    }
                }

                if (userInput.Key == ConsoleKey.S)
                {
                    if (visibilityBoard[cursorRow, cursorCol] == "0")
                    {
                        visibilityBoard[cursorRow, cursorCol] = "1";

                        if (board[cursorRow, cursorCol] == "*")
                        {
                            break;  // <--- end game logic to be implemented 
                        }
                        if (board[cursorRow, cursorCol] == "0")
                        {
                            bool[,] visited = new bool[board.GetLength(0), board.GetLength(1)];
                            OpenZeroesArea(board, visibilityBoard, cursorRow, cursorCol, visited);
                        }

                    }

                }

            }

            if (dificulty == 1)
            {
                if (CheckIfGameWon(board, visibilityBoard, 0) == 71)
                {
                    win = true;
                    break;
                }
            }
            else if (dificulty == 2)
            {
                if (CheckIfGameWon(board, visibilityBoard, 0) == 216)
                {
                    win = true;

                    break;
                }
            }
            else if (dificulty == 3)
            {
                if (CheckIfGameWon(board, visibilityBoard, 0) == 381)
                {
                    win = true;

                    break;
                }
            }

            PrintBoard(board, visibilityBoard, cursorRow, cursorCol);
            PrintMinesLeft(board, visibilityBoard);
            PrintTimeElapsed(start);
            PrintMessageOnConsole("Arrows to move, \"A\" to mark mine, \"S\" to open cell", 20, 0); // temporary 

            // catch game end and exit this loop
        }

        ////checking whether the current highscore that is saved in the file and then calls WriteHighScore(currentHighScore, currentScore, dificulty);
        if (win)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("YOU WIN");
            Console.ForegroundColor = ConsoleColor.White;

            DateTime end = DateTime.Now;
            Regex ex = new Regex("score: [0-9]+"); ///if issue with this the below listed match remove the dot "[0-9]+"
            int currentScore = EndTimeElapsed(start, end);
            int currentHighScore = 0;
            StreamReader readerToAr = new StreamReader("..\\..\\Highscores.txt");
            string[] lines = readerToAr.ReadToEnd().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            Match match = ex.Match(lines[dificulty - 1]);
            currentHighScore = int.Parse(match.ToString()
                                        .Substring(7));
            readerToAr.Close();
            WriteHighScore(currentHighScore, currentScore, dificulty);
        }

        AskForNewGame(win);
        WriteEndGameMessage();
        Console.ReadKey();
    }
}