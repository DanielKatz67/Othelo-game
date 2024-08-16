
namespace Ex02_Othelo;

public class Board
{
    private readonly int r_Width;
    private readonly int r_Height;
    private char[,] m_Grid;

    public Board(int i_Width, int i_Height)
    {
        r_Width = i_Width;
        r_Height = i_Height;
        m_Grid = new char[r_Width, r_Height];
        InitializeBoard();
    }
    
    private void InitializeBoard()
    {
        int middleWidthIndex = (int)Math.Floor((decimal)(r_Width / 2));
        int middleHeightIndex = (int)Math.Floor((decimal)(r_Height / 2));
        
        setCell(eColor.White, new Coordinate(middleWidthIndex - 1, middleHeightIndex - 1));
        setCell(eColor.White, new Coordinate(middleWidthIndex, middleHeightIndex));
        setCell(eColor.Black, new Coordinate(middleWidthIndex, middleHeightIndex - 1));
        setCell(eColor.Black, new Coordinate(middleWidthIndex - 1, middleHeightIndex));
    }

    public void SetCell(eColor i_Color, Coordinate i_Coordinate)
    {
        // TODO: validate arguments
        m_Grid[i_Coordinate.X, i_Coordinate.Y] = (char)i_Color;
    }
    
    
    // TODO: Identify two edges
    
    
    // TODO: Convert all cells between edges
    private void convertCellsBetweenEdges()
    {
        
    }

    private void setCell(eColor i_Color, Coordinate i_Coordinate)
    {
        m_Grid[i_Coordinate.X, i_Coordinate.Y] = (char)i_Color;
    }
}