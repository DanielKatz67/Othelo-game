
namespace Ex02_Othelo;

public class Board
{
    private readonly int m_Width;
    private readonly int m_Height;
    private char[,] m_Grid;

    public Board(int i_Width, int i_Height)
    {
        m_Width = i_Width;
        m_Height = i_Height;
        m_Grid = new char[m_Width, m_Height];
        InitializeBoard();
    }
    
    private void InitializeBoard()
    {
        int middleWidthIndex = (int)Math.Floor((decimal)(m_Width / 2));
        int middleHeightIndex = (int)Math.Floor((decimal)(m_Height / 2));
        
        SetCell(eColor.White, middleWidthIndex - 1, middleHeightIndex - 1);
        SetCell(eColor.White, middleWidthIndex, middleHeightIndex);
        SetCell(eColor.Black, middleWidthIndex, middleHeightIndex - 1);
        SetCell(eColor.Black, middleWidthIndex - 1, middleHeightIndex);
    }

    public void SetCell(eColor i_Color, int i_X, int i_Y)
    {
        m_Grid[i_X, i_Y] = (char)i_Color;
    }
}