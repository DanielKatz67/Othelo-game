
namespace Ex02_Othelo;

public class BoardValidator
{
    public static bool CellIsValid(Coordinate i_CellCoordinate, eColor i_CoinColor, Coordinate?[] i_EdgesInSameColor, Board i_Board)
    {
        bool isCellEmpty = i_Board.Cell(i_CellCoordinate) == '\0';
        
        return isCellEmpty && CellInGrid(i_CellCoordinate, i_Board) &&
            isValidMove(i_CellCoordinate, i_CoinColor, i_EdgesInSameColor);
    }
    
    public static bool CellInGrid(Coordinate i_CellCoordinate, Board i_Board)
    {
        return (i_CellCoordinate.X > 0 || i_CellCoordinate.X <= i_Board.Width || 
                i_CellCoordinate.Y > 0 || i_CellCoordinate.Y <= i_Board.Height);
    }
    
    private static bool isValidMove(Coordinate i_CellCoordinate, eColor i_CoinColor, Coordinate?[] i_EdgesInSameColor)
    {
        bool isValidMove = false;
        
        foreach (Coordinate? edge in i_EdgesInSameColor)
        {
            if (edge.HasValue)
            {
                isValidMove = true;
                break;
            }
        }

        return isValidMove; 
    }
    
    public static Coordinate?[] IdentifyAllEdges(Coordinate i_CellCoordinate, eColor i_CoinColor, Board i_Board)
    {
        Coordinate?[] validEdgesInAllDirections = new Coordinate?[8];
        int[,] directionVectors = Constants.sr_Directions;
        eColor opponentColor = i_CoinColor == eColor.Black ? eColor.White : eColor.Black;
        
        for (int directionIndex = 0; directionIndex < 8; directionIndex++)
        {
            int deltaX = directionVectors[directionIndex, 0];
            int deltaY = directionVectors[directionIndex, 1];
            int currentX = i_CellCoordinate.X + deltaX;
            int currentY = i_CellCoordinate.Y + deltaY;
            bool hasOpponentCoinBetween = false;
            
            while (currentX >= 0 && currentX < i_Board.Width && currentY >= 0 && currentY < i_Board.Height)
            {
                char currentCellColor =  i_Board.Cell(new Coordinate(currentX, currentY));

                if (currentCellColor == (char)opponentColor)
                {
                    hasOpponentCoinBetween = true;
                }
                else if (currentCellColor == (char)i_CoinColor)
                {
                    if (hasOpponentCoinBetween)
                    {
                        validEdgesInAllDirections[directionIndex] = new Coordinate(currentX, currentY);
                    }
                    
                    break;
                }
                else
                {
                    break;
                }

                currentX += deltaX;
                currentY += deltaY;
            }
        }

        return validEdgesInAllDirections;
    }
}