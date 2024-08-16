
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
        if (i_Coordinate.X < 0 || i_Coordinate.X >= r_Board.Width ||
            i_Coordinate.Y < 0 || i_Coordinate.Y >= r_Board.Height)
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
    
    private static bool isValidMove(Coordinate i_Coordinate, eColor i_Color)
    {
        //TODO
    }
    
    
    public static Coordinate IdentifyTwoEdges(Coordinate i_Coordinate)
    {
        // TODO: Identify two edges
    }
    
}