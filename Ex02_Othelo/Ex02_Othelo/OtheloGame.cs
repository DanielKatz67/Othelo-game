using System.Runtime.CompilerServices;

namespace Ex02_Othelo;

public static class OtheloGame
{
    private static Player m_Player1;
    private static Player m_Player2;
    private static Player m_CurrentPlayer;
    private static int m_BoardSize;
    
    public static void Run()
    {
        Console.WriteLine("Welcome to Othelo Game!");
        m_Player1 = getPlayer("Enter your name: ", eColor.Black);
        m_Player2 = getPlayer("Opponent! enter your name: ", eColor.White);
        m_BoardSize = getBoardSize();
        
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