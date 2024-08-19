namespace Ex02_Othelo;

public class Player
{
    private readonly eColor r_Color;
    private readonly string r_Name;
    private int m_Score;

    public eColor Color
    {
        get
        {
            return r_Color;
        }
    }
    
    public string Name
    {
        get
        {
            return r_Name;
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

    public Player(string i_Name, int i_Score, eColor i_Color)
    {
        r_Name = i_Name;
        m_Score = i_Score;
        r_Color = i_Color;
    }
}