namespace Ex02_Othelo;

public class Computer : Player
{
    private string m_Name;
    private eColor m_Color;
    private int m_Score;
    
    public eColor Color
    {
        get
        {
            return m_Color;
        }
    }
    
    public int Score
    {
        get
        {
            return m_Score;
        }
        set
        {
            m_Score = value;
        }
    }

    public Computer(string i_Name, int i_Score, eColor i_Color) : base(i_Name, i_Score, i_Color)
    {
        m_Score = i_Score;
        m_Color = i_Color;
        m_Name = i_Name;
    }

    public void MoveRandomly(Board i_Board)
    {
        List<Coordinate> validMoves = getValidMoves(i_Board);

        if (validMoves.Count > 0)
        {
            Random random = new Random();
            Coordinate selectedMove = validMoves[random.Next(validMoves.Count)];
            i_Board.SetCell(m_Color, selectedMove);
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
                if (BoardValidator.CellIsValid(potentialMove, m_Color, BoardValidator.IdentifyAllEdges(potentialMove, m_Color, i_Board), i_Board))
                {
                    validMoves.Add(potentialMove);
                }
            }
        }

        return validMoves;
    }
}