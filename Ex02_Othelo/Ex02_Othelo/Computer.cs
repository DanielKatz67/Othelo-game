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
            i_Board.SetCell(Color, selectedMove);
        }
    }

    private List<Coordinate> getValidMoves(Board i_Board)
    {
        List<Coordinate> validMoves = new List<Coordinate>();

        for (int x = 0; x < i_Board.Width; x++)
        {
            for (int y = 0; y < i_Board.Height; y++)
            {
                Coordinate potentialMove = new Coordinate(x, y);
                if (BoardValidator.CellIsValid(potentialMove, Color, BoardValidator.IdentifyAllEdges(potentialMove, Color, i_Board), i_Board))
                {
                    validMoves.Add(potentialMove);
                }
            }
        }

        return validMoves;
    }
}