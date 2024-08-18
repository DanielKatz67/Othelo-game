using System.Runtime.CompilerServices;

namespace Ex02_Othelo;

public static class OtheloGame
{
    private static Player m_Player1;
    private static Player m_Player2;
    private static Player m_CurrentPlayer;
    
    public static void Run()
    {
        Console.WriteLine("Welcome to Othelo Game!");
        m_Player1 = getPlayer("Enter your name: ", eColor.Black);
        m_Player2 = getPlayer("Opponent! enter your name: ", eColor.White);
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