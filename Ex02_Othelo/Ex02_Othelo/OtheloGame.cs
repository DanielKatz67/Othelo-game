using System.Runtime.CompilerServices;

namespace Ex02_Othelo;

public static class OtheloGame
{
    private static Player m_Player1;
    private static Player m_Player2;
    private static Player m_CurrentPlayer;
    private static Board m_Board;
    private static BoardValidator m_BoardValidator;
    
    public static void Run()
    {
        Console.WriteLine("Welcome to Othelo Game!");
        
        m_Player1 = getPlayer("Enter your name: ", eColor.Black);
        m_Player2 = getPlayer("Opponent! enter your name: ", eColor.White);
        m_CurrentPlayer = m_Player1;
        
        int boardSize = getBoardSize();
        m_Board = new Board(boardSize, boardSize);
        
    }

    private static bool isStepValid(string? i_Step, out Coordinate o_Coordinate)
    {
        o_Coordinate = new Coordinate();

        if (string.IsNullOrWhiteSpace(i_Step) || i_Step.Length < 2)
        {
            return false;
        }

        char columnChar = i_Step[0];
        string rowPart = i_Step.Substring(1);

        int column = columnChar - 'A';
        bool isRowValid = int.TryParse(rowPart, out int row);

        // Check if the column is within the valid range (e.g., A-H for 8x8 board)
        bool isColumnValid = column >= 0 && column < m_Board.Width;

        // Check if the row is within the valid range (1 to board height)
        isRowValid = isRowValid && row >= 1 && row <= m_Board.Height;

        if (isColumnValid && isRowValid)
        {
            // Convert row from 1-based index to 0-based index
            o_Coordinate = new Coordinate(row - 1, column);
            return true;
        }

        return false;
    }



    private static int getBoardSize()
    {
        Console.WriteLine("Enter board Size: ");
        string? boardSize = Console.ReadLine();
        int validBoardSize;
        
        while (!isValidBoardSize(boardSize, out validBoardSize))
        {
            Console.WriteLine("Invalid input, size must be a positive integer (at least 3): ");
            boardSize = Console.ReadLine();
        }

        return validBoardSize;
    }

    private static bool isValidBoardSize(string? i_BoardSize, out int o_BoardSize)
    {
        bool isValid = int.TryParse(i_BoardSize, out o_BoardSize) && o_BoardSize >= 3;
        
        return isValid;
    }
    
    private static Player getPlayer(string i_Message,eColor i_Color)
    {
        Console.WriteLine(i_Message);
        string? playerName = Console.ReadLine();
        
        while (!isNameValid(playerName))
        {
            Console.WriteLine("Invalid name, Please enter your name: ");
            playerName = Console.ReadLine();
        }

        return new Player(playerName, 0, i_Color);
    }

    private static bool isNameValid(string? i_PlayerName)
    {
        return !string.IsNullOrWhiteSpace(i_PlayerName) 
               && i_PlayerName.Length >= 3 
               && i_PlayerName.All(char.IsLetter);
    }
}