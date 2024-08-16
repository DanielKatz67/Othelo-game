
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
        
        setCell(eColor.White, middleWidthIndex - 1, middleHeightIndex - 1);
        setCell(eColor.White, middleWidthIndex, middleHeightIndex);
        setCell(eColor.Black, middleWidthIndex, middleHeightIndex - 1);
        setCell(eColor.Black, middleWidthIndex - 1, middleHeightIndex);
    }

    public void SetCell(eColor i_Color, int i_X, int i_Y)
    {
        // TODO: validate arguments
        m_Grid[i_X, i_Y] = (char)i_Color;
    }
    
    
    // TODO: Identify two edges
    
    
    // TODO: Convert all cells between edges
    private void convertCellsBetweenEdges()
    {
        
    }

    private void setCell(eColor i_Color, int i_X, int i_Y)
    {
        m_Grid[i_X, i_Y] = (char)i_Color;
    }
}