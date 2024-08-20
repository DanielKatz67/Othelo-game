namespace Ex02_Othelo;

public class Computer : Player
{
    public Computer(string i_Name, int i_Score, eColor i_Color) : base(i_Name, i_Score, i_Color)
    {
    }

    public void MoveRandomly(Board i_Board)
    {
        List<Coordinate> validMoves = getValidMoves(i_Board);

        if (validMoves.Count > 0)
        {
            Random random = new Random();
            Coordinate selectedMove = validMoves[random.Next(validMoves.Count)];
            i_Board.TrySetCell(Color, selectedMove);
        }
    }

    private List<Coordinate> getValidMoves(Board i_Board)
    {
        List<Coordinate> validMoves = new List<Coordinate>();

        for (int columnIndex = 0; columnIndex < i_Board.Width; columnIndex++)
        {
            for (int rowIndex = 0; rowIndex < i_Board.Height; rowIndex++)
            {
                Coordinate potentialMove = new Coordinate(columnIndex, rowIndex);
                
                if (BoardValidator.CellIsValid(potentialMove, Color, BoardValidator.IdentifyAllEdges(potentialMove, Color, i_Board), i_Board))
                {
                    validMoves.Add(potentialMove);
                }
            }
        }

        return validMoves;
    }
}