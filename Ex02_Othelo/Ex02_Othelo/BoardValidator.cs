
namespace Ex02_Othelo;

public class BoardValidator
{
    private readonly Board r_Board;

    public BoardValidator(Board i_Board)
    {
        r_Board = i_Board;
    }
    
    public Board Board
    {
        get
        {
            return r_Board;
        }
    }
    
    public bool CellIsValid(Coordinate i_Coordinate, eColor i_Color)
    {
        if (!CellInGrid())
        {
            //ToDo: GameUI.OutOfGridMassage();
            return false;
        }
        
        if (r_Board.Cell(i_Coordinate) != '\0')
        {
            return false;
        }
        
        return isValidMove(i_Coordinate, i_Color);
    }
    
    public bool CellInGrid(Coordinate i_Coordinate)
    {
        return (i_Coordinate.X < 0 || i_Coordinate.X >= r_Board.Width || 
                i_Coordinate.Y < 0 || i_Coordinate.Y >= r_Board.Height);
    }
    
    private bool isValidMove(Coordinate i_Coordinate, eColor i_Color)
    {
        Coordinate?[] edges = IdentifyAllEdges(i_Coordinate, i_Color);
        
        foreach (Coordinate? edge in edges)
        {
            if (edge.HasValue)
            {
                return true;
            }
        }

        return false; 
    }
    
    public Coordinate?[] IdentifyAllEdges(Coordinate i_Coordinate, eColor i_Color)
    {
        Coordinate?[] validEdgesInAllDirections = new Coordinate?[8];
        
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
        
        eColor opponentColor = i_Color == eColor.Black ? eColor.White : eColor.Black;
        
        for (int i = 0; i < 8; i++)
        {
            int dx = directions[i, 0];
            int dy = directions[i, 1];
            int x = i_Coordinate.X + dx;
            int y = i_Coordinate.Y + dy;
            bool hasOpponentCoinBetween = false;
            
            while (x >= 0 && x < Board.Width && y >= 0 && y <  Board.Height)
            {
                char currentCell =  Board.Cell(new Coordinate(x, y));

                if (currentCell == (char)opponentColor)
                {
                    hasOpponentCoinBetween = true;
                }
                else if (currentCell == (char)i_Color)
                {
                    if (hasOpponentCoinBetween)
                    {
                        validEdgesInAllDirections[i] = new Coordinate(x, y);
                    }
                    break;
                }
                else
                {
                    break;
                }

                x += dx;
                y += dy;
            }
        }

        return validEdgesInAllDirections;
    }
}