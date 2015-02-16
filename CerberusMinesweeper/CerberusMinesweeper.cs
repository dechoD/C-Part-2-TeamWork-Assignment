using System;

class CerberusMinesweeper
{
    /// <summary>
    /// Prints a message on the console
    /// </summary>
    /// <param name="message"> the message to be printed </param>
    /// <param name="xCoord"> x coordinate of the console position </param>
    /// <param name="yCoord"> y coordinate of the console position </param>
    static void PrintMessageOnConsole(string message, int xCoord, int yCoord)
    { 
        // TODO: Implement message printing on the console
    }

    /// <summary>
    /// Method used to print a difficulty menu on the console
    /// </summary>
    /// <param name="xCord"> x position of the upper left corner of the menu </param>
    /// <param name="yCoord"> y position of the upper left corner of the menu </param>
    /// <returns> An integer representing the difficuilty </returns>    
    static int PrintDifficultyMenu(int xCord, int yCoord)
    {
        // Misho will work here ..
        return 0;
    }

    /// <summary>
    /// Prints a mines left count
    /// </summary>
    /// <param name="array"> The array with the mines and numbers </param>
    static void PrintMinesLeft(string[,] array)
    {
        // TODO: Implement method to print the mines left somewhere using PrintMessageOnConsole Method
    }

    /// <summary>
    /// Keeping count of the seconds after the game has started    
    /// </summary>
    /// <param name="start"> the time the game started </param>
    static void PrintTimeElapsed(DateTime start)
    { 
        // TODO: Implement method to print the time since the game started
    }

    /// <summary>
    /// Draws the board
    /// </summary>
    /// <param name="minesArray"> the array with the mines and numbers</param>
    /// <param name="visibilityArray"> the array with the information about what is visible to the player </param>
    static void PrintBoard(string[,] minesArray, int[,] visibilityArray)
    {
        // TODO: Implement drawing of the board     
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
        for (int row  = 0; row  < array.GetLength(0); row ++)
        {
            for (int col = 0; col < array.GetLength(1); col++)
            {
                Console.Write("{0,2}", array[row, col]);
            }

            Console.WriteLine();
        }
    }

    /// <summary>
    /// Populates an array with mines at randomly generated positions
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
        else
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
    /// Fills the cells around the mines with the right numbers
    /// </summary>
    /// <param name="array"> the array with the mines and the numbers </param>
    static void CalculateDigitsArroundMines(string[,] array)
    {
        // TODO:Implement generation of digits arround mines generated by FillWithRandomMines Method
    }

    /// <summary>
    /// Write end game message with high score alert if any
    /// </summary>
    /// <param name="scoreFilePath"> path to the file with the high scores </param>
    static void WriteEndGameMessage(string scoreFilePath)
    {
        // TODO: Implement message print for end of the game (win and lose)
    }

    /// <summary>
    /// Write high scores after the game has finished
    /// </summary>
    /// <param name="scoreFilePath"> path to the file keeping the high scores </param>
    static void WriteHighScore(string scoreFilePath)
    { 
        // TODO: Implement high scores print on the console with current highscore in different color
    }

    /// <summary>
    /// Ask the user if he wants new game
    /// </summary>
    static void AskForNewGame()
    {
        // TODO: Implement logic for new game question to user and exit if negative
    }

    static void Main()
    {
        //// main loop - exit only after negative answer in AskForNewGame() method

        //// new game start
        //PrintDifficultyMenu();
        string[,] board = CreateBoard(2);
        DebugPrintBoard(board);
        Console.WriteLine();
        FillWithRandomMines(board, 2);
        DebugPrintBoard(board);
        //CalculateDigitsArroundMines();

        //// start of current game loop
        //PrintBoard();
        //PrintMinesLeft();
        //PrintTimeElapsed();
        // catch movement and clicks

        //// game end
        //WriteEndGameMessage();
        //WriteHighScore();
        //AskForNewGame();
    }
}