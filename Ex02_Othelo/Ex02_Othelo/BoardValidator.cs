
namespace Ex02_Othelo;

public class BoardValidator
{
    public static bool CellIsValid(Coordinate i_Coordinate, eColor i_Color, Coordinate?[] i_EdgesInSameColor, Board i_Board)
    {
        if (!CellInGrid(i_Coordinate, i_Board))
        {
            return false;
        }
        
        if (i_Board.Cell(i_Coordinate) != '\0')
        {
            return false;
        }
        
        return isValidMove(i_Coordinate, i_Color, i_EdgesInSameColor);
    }
    
    public static bool CellInGrid(Coordinate i_Coordinate, Board i_Board)
    {
        return (i_Coordinate.X > 0 || i_Coordinate.X <= i_Board.Width || 
                i_Coordinate.Y > 0 || i_Coordinate.Y <= i_Board.Height);
    }
    
    private static bool isValidMove(Coordinate i_Coordinate, eColor i_Color, Coordinate?[] i_EdgesInSameColor)
    {
        foreach (Coordinate? edge in i_EdgesInSameColor)
        {
            if (edge.HasValue)
            {
                return true;
            }
        }

        return false; 
    }
    
    public static Coordinate?[] IdentifyAllEdges(Coordinate i_Coordinate, eColor i_Color, Board i_Board)
    {
        Coordinate?[] validEdgesInAllDirections = new Coordinate?[8];
        int[,] directions = Constants.directions;
        eColor opponentColor = i_Color == eColor.Black ? eColor.White : eColor.Black;
        
        for (int i = 0; i < 8; i++)
        {
            int directionX = directions[i, 0];
            int directionY = directions[i, 1];
            int x = i_Coordinate.X + directionX;
            int y = i_Coordinate.Y + directionY;
            bool hasOpponentCoinBetween = false;
            
            while (x >= 0 && x < i_Board.Width && y >= 0 && y < i_Board.Height)
            {
                char currentCell =  i_Board.Cell(new Coordinate(x, y));

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

                x += directionX;
                y += directionY;
            }
        }

        return validEdgesInAllDirections;
    }
}