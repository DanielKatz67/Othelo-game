
namespace Ex02_Othelo;

public class Board
{
    private readonly int r_BoardWidth;
    private readonly int r_BoardHeight;
    private char[,] m_Grid;

    public Board(int iBoardWidth, int iBoardHeight)
    {
        r_BoardWidth = iBoardWidth;
        r_BoardHeight = iBoardHeight;
        m_Grid = new char[r_BoardWidth, r_BoardHeight];
        initializeBoard();
    }
    
    public int Width
    {
        get
        {
            return r_BoardWidth;
        }
    }
    
    public int Height
    {
        get
        {
            return r_BoardHeight;
        }
    }
    
    public char Cell(Coordinate i_CellCoordinate)
    {
        return m_Grid[i_CellCoordinate.X, i_CellCoordinate.Y];
    }
    
    private void initializeBoard()
    {
        int middleWidthIndex = (int)Math.Floor((decimal)(r_BoardWidth / 2));
        int middleHeightIndex = (int)Math.Floor((decimal)(r_BoardHeight / 2));
        
        setCellForInit(eColor.White, new Coordinate(middleWidthIndex - 1, middleHeightIndex - 1));
        setCellForInit(eColor.White, new Coordinate(middleWidthIndex, middleHeightIndex));
        setCellForInit(eColor.Black, new Coordinate(middleWidthIndex, middleHeightIndex - 1));
        setCellForInit(eColor.Black, new Coordinate(middleWidthIndex - 1, middleHeightIndex));
    }

    private void setCellForInit(eColor i_ColorToSet, Coordinate i_CellCoordinateToSet)
    {
        m_Grid[i_CellCoordinateToSet.X, i_CellCoordinateToSet.Y] = (char)i_ColorToSet;
    }
    
    public bool TrySetCell(eColor i_ColorToSet, Coordinate i_CellCoordinateToSet)
    {
        Coordinate?[] edgesInSameColor = BoardValidator.IdentifyAllEdges(i_CellCoordinateToSet, i_ColorToSet, this);
        bool isSetSucceeded;
        
        if (BoardValidator.CellIsValid(i_CellCoordinateToSet, i_ColorToSet, edgesInSameColor, this))
        {
            m_Grid[i_CellCoordinateToSet.X, i_CellCoordinateToSet.Y] = (char)i_ColorToSet;
            convertCellsBetweenEdges(i_ColorToSet, i_CellCoordinateToSet, edgesInSameColor);
            isSetSucceeded = true;
        }
        else
        { 
            isSetSucceeded = false; 
        }
        
        return isSetSucceeded;
    }
    
    private void convertCellsBetweenEdges(eColor i_ColorToConvert, Coordinate i_OriginChangedCoordinate, Coordinate?[] i_EdgesInSameColor)
    {
        int[,] directionVectors = Constants.sr_Directions;
        
        for (int directionIndex = 0; directionIndex < 8; directionIndex++)
        {
            if (i_EdgesInSameColor[directionIndex].HasValue)
            {
                int deltaX = directionVectors[directionIndex, 0];
                int deltaY = directionVectors[directionIndex, 1];
                int currentX = i_OriginChangedCoordinate.X + deltaX;
                int currentY = i_OriginChangedCoordinate.Y + deltaY;
                
                while (currentX != i_EdgesInSameColor[directionIndex].Value.X || currentY != i_EdgesInSameColor[directionIndex].Value.Y)
                {
                    m_Grid[currentX, currentY] = (char)i_ColorToConvert;
                    currentX += deltaX;
                    currentY += deltaY;
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
        
        for (int rowIndex = 0; rowIndex < r_BoardWidth; rowIndex++)
        {
            printSeparatorLine();
            printRow(rowIndex);
        }

        printSeparatorLine();
    }
    
    private void printColumnHeaders()
    {
        Console.Write("  ");
        
        for (char columnHeader = 'A'; columnHeader < 'A' + r_BoardWidth; columnHeader++)
        {
            Console.Write("  " + columnHeader + " ");
        }
        
        Console.WriteLine();
    }
    
    private void printSeparatorLine()
    {
        Console.Write("  ");
        
        for (int columnIndex = 0; columnIndex < r_BoardWidth; columnIndex++)
        {
            Console.Write("====");
        }
        
        Console.WriteLine("=");
    }
    
    private void printRow(int i_RowIndex)
    {
        Console.Write((i_RowIndex + 1) + " ");
        char cellContent;
        
        for (int columnIndex = 0; columnIndex < r_BoardHeight; columnIndex++)
        {
            cellContent = m_Grid[i_RowIndex, columnIndex] == '\0' ? ' ' : m_Grid[i_RowIndex, columnIndex];
            Console.Write("| " + cellContent + " ");
        }
            
        Console.WriteLine("|");
    }
    
    public void CalculateScores(Player i_Player1, Player i_Player2)
    {
        int blackScore = 0;
        int whiteScore = 0;

        for (int columnIndex = 0; columnIndex < r_BoardWidth; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < r_BoardHeight; rowIndex++)
            {
                if (m_Grid[columnIndex, rowIndex] == (char)eColor.Black)
                {
                    blackScore++;
                }
                else if (m_Grid[columnIndex, rowIndex] == (char)eColor.White)
                {
                    whiteScore++;
                }
            }
        }

        i_Player1.Score = blackScore;
        i_Player2.Score = whiteScore;
    }
}