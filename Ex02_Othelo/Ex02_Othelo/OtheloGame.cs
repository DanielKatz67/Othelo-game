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
        
        startGame();
    }
    
    private static void startGame()
    {
        while (!isGameOver())
        {
            Console.Clear();
            m_Board.PrintBoard();
            Console.WriteLine($"{m_CurrentPlayer.Name} ({(char)m_CurrentPlayer.Color}), Enter your move (e.g A1): ");
            string? step = Console.ReadLine();
            Coordinate coordinate;
            
            while (!isValidMove(step, out coordinate))
            {
                Console.WriteLine("Invalid move. Please enter a valid move (e.g., A1):");
                step = Console.ReadLine();
            }
            
            m_Board.SetCell(m_CurrentPlayer.Color, coordinate);
            m_CurrentPlayer.Score++;
            
            switchPlayers();
        }
        
        endGame();
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
    
    private static bool isGameOver()
    {
        return !hasValidMoves(m_Player1) || 
               !hasValidMoves(m_Player2);
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
        m_CurrentPlayer = m_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;
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
    
    private static void endGame()
    {
        m_Board.PrintBoard();
        Console.WriteLine($"Game Over! Final Scores: {m_Player1.Name} (X): {m_Player1.Score}, {m_Player2.Name} (O): {m_Player2.Score}");
        Console.WriteLine($"{(m_Player1.Score > m_Player2.Score ? m_Player1.Name : m_Player2.Name)} wins!");
        Console.WriteLine("Do you want to play again? (yes/no):");
        
        if (Console.ReadLine().Trim().ToLower() == "yes")
        {
            Run();
        }
    }
}