
namespace Ex02_Othelo;

public class BoardValidator
{
    public static bool CellIsValid(Coordinate i_Coordinate, eColor i_Color, Coordinate?[] i_edgesInSameColor, Board i_board)
    {
        if (!CellInGrid(i_Coordinate, i_board))
        {
            //ToDo: GameUI.OutOfGridMassage();
            return false;
        }
        
        if (i_board.Cell(i_Coordinate) != '\0')
        {
            //ToDo: GameUI.NotEmptyCellMassage();
            return false;
        }
        
        return isValidMove(i_Coordinate, i_Color, i_edgesInSameColor);
    }
    
    public static bool CellInGrid(Coordinate i_Coordinate, Board i_board)
    {
        return (i_Coordinate.X > 0 || i_Coordinate.X <= i_board.Width || 
                i_Coordinate.Y > 0 || i_Coordinate.Y <= i_board.Height);
    }
    
    private static bool isValidMove(Coordinate i_Coordinate, eColor i_Color, Coordinate?[] i_edgesInSameColor)
    {
        foreach (Coordinate? edge in i_edgesInSameColor)
        {
            if (edge.HasValue)
            {
                return true;
            }
        }

        return false; 
    }
    
    public static Coordinate?[] IdentifyAllEdges(Coordinate i_Coordinate, eColor i_Color, Board i_board)
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
            
            while (x >= 0 && x < i_board.Width && y >= 0 && y < i_board.Height)
            {
                char currentCell =  i_board.Cell(new Coordinate(x, y));

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