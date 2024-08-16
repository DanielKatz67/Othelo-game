namespace Ex02_Othelo;

public struct Coordinate
{
    private int m_x;
    private int m_y;
    
    public Coordinate(int i_X, int i_Y)
    {
        m_x = i_X;
        m_y = i_Y;
    }
    
    public int X
    {
        get
        {
            return m_x;
        }
    }
    
    public int Y
    {
        get
        {
            return m_y;
        }
    }
}