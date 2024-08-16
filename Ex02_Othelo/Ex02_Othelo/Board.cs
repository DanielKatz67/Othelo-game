namespace Ex02_Othelo;

public class Board
{
    private int m_Size;
    private char[,] m_Grid;

    public Board(int i_Size, char[,] i_Grid)
    {
        m_Size = i_Size;
        m_Grid = i_Grid;
    }
}