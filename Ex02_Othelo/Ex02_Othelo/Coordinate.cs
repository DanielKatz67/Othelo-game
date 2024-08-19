namespace Ex02_Othelo;

public struct Coordinate
{
    private readonly int r_X;
    private readonly int r_Y;
    
    public Coordinate(int i_X, int i_Y)
    {
        r_X = i_X;
        r_Y = i_Y;
    }
    
    public int X
    {
        get
        {
            return r_X;
        }
    }
    
    public int Y
    {
        get
        {
            return r_Y;
        }
    }
}