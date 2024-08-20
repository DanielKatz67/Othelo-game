namespace Ex02_Othelo;

public class OtheloGame
{
    private Player m_Player1;
    private Player m_Player2;
    private Computer m_Computer;
    private Player m_CurrentPlayer;
    private Board m_Board;
    private BoardValidator m_BoardValidator;
    private string m_PlayerHasNoMovesNotification = "";
    private bool m_IsQuit;
    private bool m_IsPlayingAgainstComputer;

    public void Run()
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

    private bool askIfPlayAgainstComputer()
    {
        Console.WriteLine("Do you want to play against the computer? (yes/any other key for human opponent):");
        string? input = Console.ReadLine().Trim().ToLower();

        return input == "yes";
    }

    private void startGame()
    {
        while (!m_IsQuit && !isGameOver())
        {
            displayBoardAndPrompt();

            if (m_IsPlayingAgainstComputer && m_CurrentPlayer == m_Computer)
            {
                m_Computer.MoveRandomly(m_Board);
                switchPlayers();
                continue;
            }

            string? currentPlayerMove = Console.ReadLine();

            if (isQuitCommand(currentPlayerMove))
            {
                m_IsQuit = true;
                break;
            }

            if (!isGameContinuedAfterMove(currentPlayerMove, out Coordinate coordinate))
            {
                m_IsQuit = true;
                break;
            }

            m_Board.TrySetCell(m_CurrentPlayer.Color, coordinate);
            switchPlayers();
        }
        
        handleGameEnd();
    }

    private void displayBoardAndPrompt()
    {
        m_Board.PrintBoard();
        Console.WriteLine(m_PlayerHasNoMovesNotification);
        Console.WriteLine($"{m_CurrentPlayer.Name} ({(char)m_CurrentPlayer.Color}), Enter your move (e.g A1) or press 'Q' to quit:");
    }

    private static bool isQuitCommand(string? i_InputMove)
    {
        return i_InputMove?.Trim().ToUpper() == "Q";
    }

    private bool isGameContinuedAfterMove(string? i_InputMove, out Coordinate o_CellCoordinate)
    {
        bool isGameContinue = true;
        
        while (!isValidMove(i_InputMove, out o_CellCoordinate))
        {
            Console.WriteLine("Invalid move. Please enter a valid move (e.g., A1) or press 'Q' to quit:");
            i_InputMove = Console.ReadLine();

            if (isQuitCommand(i_InputMove))
            {
                isGameContinue = false;
                break;
            }
        }

        return isGameContinue;
    }

    private void handleGameEnd()
    {
        m_Board.PrintBoard();

        if (m_IsPlayingAgainstComputer)
        {
            m_Board.CalculateScores(m_Player1, m_Computer);
            Console.WriteLine($"Game Over! Final Scores: {m_Player1.Name} (X): {m_Player1.Score}, Computer (O): {m_Computer.Score}");
            printResults(m_Player1, m_Computer);
        }
        else
        {
            m_Board.CalculateScores(m_Player1, m_Player2);
            Console.WriteLine($"Game Over! Final Scores: {m_Player1.Name} (X): {m_Player1.Score}, {m_Player2.Name} (O): {m_Player2.Score}");
            printResults(m_Player1, m_Player2);
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

    private void printResults(Player i_Player1, Player i_Player2)
    {
        if (i_Player1.Score == i_Player2.Score)
        {
            Console.WriteLine("You both are WINNERS");
        }
        else
        {
            Console.WriteLine($"{(i_Player1.Score > i_Player2.Score ? i_Player1.Name : i_Player2.Name)} wins!");
        }
    }

    private void printGoodbye()
    {
        Console.Clear();
        Console.WriteLine("Thanks for playing!");
        Console.WriteLine("We'll be here when you're ready for another round.");
        Console.WriteLine("Take care!");
    }

    private bool isValidMove(string? i_InputMove, out Coordinate o_CellCoordinate)
    {
        return isStepValid(i_InputMove, out o_CellCoordinate) &&
               isValidCell(o_CellCoordinate, m_CurrentPlayer);
    }
    
    private bool isValidCell(Coordinate i_CellCoordinate, Player i_Player)
    {
        return BoardValidator.CellIsValid(i_CellCoordinate, i_Player.Color, 
            BoardValidator.IdentifyAllEdges(i_CellCoordinate, i_Player.Color, m_Board), 
            m_Board);
    }
    
    private bool isStepValid(string? i_InputMove, out Coordinate o_CellCoordinate)
    {
        o_CellCoordinate = new Coordinate();
        bool isStepValid = false;

        if (string.IsNullOrWhiteSpace(i_InputMove) || i_InputMove.Length < 2)
        {
            isStepValid = false;
        }
        else
        {
            char columnChar = i_InputMove[0];
            string rowPart = i_InputMove.Substring(1);
            int column = columnChar - 'A';
            bool isRowValid = int.TryParse(rowPart, out int row);
            bool isColumnValid = column >= 0 && column < m_Board.Width;
            isRowValid = isRowValid && row >= 1 && row <= m_Board.Height;

            if (isColumnValid && isRowValid)
            {
                o_CellCoordinate = new Coordinate(row - 1, column);
                isStepValid = true;
            }
        }
        
        return isStepValid;
    }
    
    private bool isGameOver()
    {
        return !hasValidMoves(m_Player1) &&
               !hasValidMoves(m_IsPlayingAgainstComputer ? m_Computer : m_Player2);
    }
    
    private bool hasValidMoves(Player i_Player)
    {
        bool hasValidMoves = false;
        
        for (int rowIndex = 0; rowIndex < m_Board.Height; rowIndex++)
        {
            for (int columnIndex = 0; columnIndex < m_Board.Width; columnIndex++)
            {
                if (isValidCell(new Coordinate(columnIndex, rowIndex), i_Player))
                {
                    hasValidMoves = true;
                }
            }
        }
        
        return hasValidMoves;
    }

    private void switchPlayers()
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

    private int getBoardSize()
    {
        Console.WriteLine("Enter board Size: ");
        string? inputBoardSize = Console.ReadLine();
        int parsedBoardSize;
        
        while (!isValidBoardSize(inputBoardSize, out parsedBoardSize))
        {
            Console.WriteLine("Invalid input, square size must be 6 or 8: ");
            inputBoardSize = Console.ReadLine();
        }

        return parsedBoardSize;
    }

    private bool isValidBoardSize(string? i_inputBoardSize, out int o_parsedBoardSize)
    {
        bool isValid = int.TryParse(i_inputBoardSize, out o_parsedBoardSize) && (o_parsedBoardSize == 6 || o_parsedBoardSize == 8);
        
        return isValid;
    }
    
    private Player getPlayer(string i_Message, eColor i_Color)
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

    private bool isNameValid(string? i_PlayerName)
    {
        return !string.IsNullOrWhiteSpace(i_PlayerName) 
               && i_PlayerName.Length >= 3 
               && i_PlayerName.All(char.IsLetter);
    }
}
