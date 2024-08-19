
namespace Ex02_Othelo;

public class Board
{
    private readonly int r_Width;
    private readonly int r_Height;
    private char[,] m_Grid;

    public Board(int i_Width, int i_Height)
    {
        r_Width = i_Width;
        r_Height = i_Height;
        m_Grid = new char[r_Width, r_Height];
        initializeBoard();
    }
    
    public int Width
    {
        get
        {
            return r_Width;
        }
    }
    
    public int Height
    {
        get
        {
            return r_Height;
        }
    }
    
    public char Cell(Coordinate i_Coordinate)
    {
        return m_Grid[i_Coordinate.X, i_Coordinate.Y];
    }
    
    private void initializeBoard()
    {
        int middleWidthIndex = (int)Math.Floor((decimal)(r_Width / 2));
        int middleHeightIndex = (int)Math.Floor((decimal)(r_Height / 2));
        
        setCellForInit(eColor.White, new Coordinate(middleWidthIndex - 1, middleHeightIndex - 1));
        setCellForInit(eColor.White, new Coordinate(middleWidthIndex, middleHeightIndex));
        setCellForInit(eColor.Black, new Coordinate(middleWidthIndex, middleHeightIndex - 1));
        setCellForInit(eColor.Black, new Coordinate(middleWidthIndex - 1, middleHeightIndex));
    }

    private void setCellForInit(eColor i_Color, Coordinate i_Coordinate)
    {
        m_Grid[i_Coordinate.X, i_Coordinate.Y] = (char)i_Color;
    }
    
    public void SetCell(eColor i_Color, Coordinate i_Coordinate)
    {
        Coordinate?[] edgesInSameColor = BoardValidator.IdentifyAllEdges(i_Coordinate, i_Color, this);
        
        if(BoardValidator.CellIsValid(i_Coordinate, i_Color, edgesInSameColor, this))
        {
            m_Grid[i_Coordinate.X, i_Coordinate.Y] = (char)i_Color;
            convertCellsBetweenEdges(i_Color, i_Coordinate, edgesInSameColor);
        }
        else
        {
            //ToDo: GameUI.InValidCellMassage();
        }
    }
    
    private void convertCellsBetweenEdges(eColor i_Color, Coordinate i_Coordinate, Coordinate?[] i_EdgesInSameColor)
    {
        int[,] directions = new int[,]
        {
            {-1,  0}, // Left
            { 1,  0}, // Right
            { 0, -1}, // Up
            { 0,  1}, // Down
            {-1, -1}, // Top-left diagonal
            { 1,  1}, // Bottom-right diagonal
            {-1,  1}, // Bottom-left diagonal
            { 1, -1}  // Top-right diagonal
        };
        
        for (int i = 0; i < 8; i++)
        {
            if (i_EdgesInSameColor[i].HasValue)
            {
                int dx = directions[i, 0];
                int dy = directions[i, 1];
                int x = i_Coordinate.X + dx;
                int y = i_Coordinate.Y + dy;
                
                while (x != i_EdgesInSameColor[i].Value.X || y != i_EdgesInSameColor[i].Value.Y)
                {
                    m_Grid[x, y] = (char)i_Color;
                    x += dx;
                    y += dy;
                }
            }
        }
    }

    public void PrintBoard()
    {
        // Todo: replace in windows
        //Ex02.ConsoleUtils.Screen.Clear();
        // in Mac:
        Console.Clear();
        printColumnHeaders();
        
        for (int x = 0; x < r_Width; x++)
        {
            printSeparatorLine();
            printRow(x);
        }

        printSeparatorLine();
    }
    
    private void printColumnHeaders()
    {
        Console.Write("  ");
        
        for (char column = 'A'; column < 'A' + r_Width; column++)
        {
            Console.Write("  " + column + " ");
        }
        
        Console.WriteLine();
    }
    
    private void printSeparatorLine()
    {
        Console.Write("  ");
        
        for (int x = 0; x < r_Width; x++)
        {
            Console.Write("====");
        }
        
        Console.WriteLine("=");
    }
    
    private void printRow(int i_X)
    {
        Console.Write((i_X + 1) + " ");
        char signToRight;
        
        for (int y = 0; y < r_Height; y++)
        {
            signToRight = m_Grid[i_X, y] == '\0' ? ' ' : m_Grid[i_X, y];
            Console.Write("| " + signToRight + " ");
        }
            
        Console.WriteLine("|");
    }
    
    public void CalculateScores(Player i_Player1, Player i_Player2)
    {
        int blackScore = 0;
        int whiteScore = 0;

        for (int x = 0; x < r_Width; x++)
        {
            for (int y = 0; y < r_Height; y++)
            {
                if (m_Grid[x, y] == (char)eColor.Black)
                {
                    blackScore++;
                }
                else if (m_Grid[x, y] == (char)eColor.White)
                {
                    whiteScore++;
                }
            }
        }

        i_Player1.Score = blackScore;
        i_Player2.Score = whiteScore;
    }
}