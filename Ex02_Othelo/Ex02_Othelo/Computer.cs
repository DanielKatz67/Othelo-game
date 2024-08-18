namespace Ex02_Othelo;

public class Computer
{
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

    public Computer(string i_Name, int i_Score, eColor i_Color)
    {
        m_Score = i_Score;
        m_Color = i_Color;
    }

    public void ImplementRandomMove()
    {
        throw new NotImplementedException();
    }
}