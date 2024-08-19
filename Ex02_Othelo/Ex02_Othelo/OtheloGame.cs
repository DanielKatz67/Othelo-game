namespace Ex02_Othelo;

public static class OtheloGame
{
    private static Player m_Player1;
    private static Player m_Player2;
    private static Computer m_Computer;
    private static Player m_CurrentPlayer;
    private static Board m_Board;
    private static BoardValidator m_BoardValidator;
    private static string m_PlayerHasNoMovesNotification = "";
    private static bool m_IsQuit = false;
    private static bool m_IsPlayingAgainstComputer = false;
    
    public static void Run()
    {
        Console.WriteLine("Welcome to Othelo Game!");
        m_Player1 = getPlayer("Enter your name: ", eColor.Black);
        m_CurrentPlayer = m_Player1;

        if (askIfPlayAgainstComputer())
        {
            m_IsPlayingAgainstComputer = true;
            m_Computer = new Computer("Computer", 0, eColor.White);
        }
        else
        {
            m_Player2 = getPlayer("Opponent! enter your name: ", eColor.White);
        }

        int boardSize = getBoardSize();
        m_Board = new Board(boardSize, boardSize);
        
        startGame();
    }

    private static bool askIfPlayAgainstComputer()
    {
        Console.WriteLine("Do you want to play against the computer? (yes/any other key for human opponent):");
        string? input = Console.ReadLine().Trim().ToLower();

        return input == "yes";
    }

    private static void startGame()
    {
        while (!m_IsQuit && !isGameOver())
        {
            displayBoardAndPrompt();

            if (m_IsPlayingAgainstComputer && m_CurrentPlayer == m_Computer)
            {
                m_Computer.MoveRandomly(m_Board);
                continue;
            }

            string? step = Console.ReadLine();

            if (isQuitCommand(step))
            {
                m_IsQuit = true;
                break;
            }

            Coordinate coordinate;

            if (!handleMoveInput(step, out coordinate))
            {
                m_IsQuit = true;
                break;
            }

            m_Board.SetCell(m_CurrentPlayer.Color, coordinate);
            switchPlayers();
        }
        
        handleGameEnd();
    }

    private static void displayBoardAndPrompt()
    {
        m_Board.PrintBoard();
        Console.WriteLine(m_PlayerHasNoMovesNotification);
        Console.WriteLine($"{m_CurrentPlayer.Name} ({(char)m_CurrentPlayer.Color}), Enter your move (e.g A1) or press 'Q' to quit:");
    }

    private static bool isQuitCommand(string? i_Step)
    {
        return i_Step?.Trim().ToUpper() == "Q";
    }

    private static bool handleMoveInput(string? i_Step, out Coordinate o_Coordinate)
    {
        while (!isValidMove(i_Step, out o_Coordinate))
        {
            Console.WriteLine("Invalid move. Please enter a valid move (e.g., A1) or press 'Q' to quit:");
            i_Step = Console.ReadLine();

            if (isQuitCommand(i_Step))
            {
                return false;
            }
        }

        return true;
    }

    private static void handleGameEnd()
    {
        m_Board.PrintBoard();

        if (m_IsPlayingAgainstComputer)
        {
            m_Board.CalculateScores(m_Player1, m_Computer);
            Console.WriteLine($"Game Over! Final Scores: {m_Player1.Name} (X): {m_Player1.Score}, Computer (O): {m_Computer.Score}");
            Console.WriteLine($"{(m_Player1.Score > m_Computer.Score ? m_Player1.Name : "Computer")} wins!");
        }
        else
        {
            m_Board.CalculateScores(m_Player1, m_Player2);
            Console.WriteLine($"Game Over! Final Scores: {m_Player1.Name} (X): {m_Player1.Score}, {m_Player2.Name} (O): {m_Player2.Score}");
            Console.WriteLine($"{(m_Player1.Score > m_Player2.Score ? m_Player1.Name : m_Player2.Name)} wins!");
        }

        Console.WriteLine("Do you want to quit? (Q/any other key to new game):");
        
        if (Console.ReadLine().Trim().ToLower() == "q")
        {
            printGoodbye();
        }
        else
        {
            m_IsPlayingAgainstComputer = false;
            Console.Clear();
            Run();
        }
    }

    private static void printGoodbye()
    {
        Console.Clear();
        Console.WriteLine("Thanks for playing!");
        Console.WriteLine("We'll be here when you're ready for another round.");
        Console.WriteLine("Take care!");
    }

    private static bool isValidMove(string? i_Step, out Coordinate o_Coordinate)
    {
        return isStepValid(i_Step, out o_Coordinate) &&
               isValidCell(o_Coordinate, m_CurrentPlayer);
    }
    
    private static bool isValidCell(Coordinate i_Coordinate, Player i_Player)
    {
        return BoardValidator.CellIsValid(i_Coordinate, i_Player.Color, 
            BoardValidator.IdentifyAllEdges(i_Coordinate, i_Player.Color, m_Board), 
            m_Board);
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
        bool isColumnValid = column >= 0 && column < m_Board.Width;
        isRowValid = isRowValid && row >= 1 && row <= m_Board.Height;

        if (isColumnValid && isRowValid)
        {
            o_Coordinate = new Coordinate(row - 1, column);
            return true;
        }

        return false;
    }
    
    private static bool isGameOver()
    {
        return !hasValidMoves(m_Player1) &&
               !hasValidMoves(m_IsPlayingAgainstComputer ? m_Computer : m_Player2);
    }
    
    private static bool hasValidMoves(Player i_Player)
    {
        for (int i = 0; i < m_Board.Width; i++)
        {
            for (int j = 0; j < m_Board.Height; j++)
            {
                if (isValidCell(new Coordinate(i, j), i_Player))
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    private static void switchPlayers()
    {
        if (m_CurrentPlayer == m_Player1 && hasValidMoves(m_IsPlayingAgainstComputer ? m_Computer : m_Player2))
        {
            m_CurrentPlayer = m_IsPlayingAgainstComputer ? (Player)m_Computer : m_Player2;
            m_PlayerHasNoMovesNotification = "";
        }
        else if (m_CurrentPlayer != m_Player1 && hasValidMoves(m_Player1))
        {
            m_CurrentPlayer = m_Player1;
            m_PlayerHasNoMovesNotification = "";
        }
        else
        {
            m_PlayerHasNoMovesNotification = $"{(m_CurrentPlayer == m_Player1 ? m_Player2?.Name ?? "Computer" : m_Player1.Name)} has no valid moves left.";
        }
    }

    private static int getBoardSize()
    {
        Console.WriteLine("Enter board Size: ");
        string? boardSize = Console.ReadLine();
        int validBoardSize;
        
        while (!isValidBoardSize(boardSize, out validBoardSize))
        {
            Console.WriteLine("Invalid input, square size must be 6 or 8: ");
            boardSize = Console.ReadLine();
        }

        return validBoardSize;
    }

    private static bool isValidBoardSize(string? i_BoardSize, out int o_BoardSize)
    {
        bool isValid = int.TryParse(i_BoardSize, out o_BoardSize) && (o_BoardSize == 6 || o_BoardSize == 8);
        
        return isValid;
    }
    
    private static Player getPlayer(string i_Message, eColor i_Color)
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
