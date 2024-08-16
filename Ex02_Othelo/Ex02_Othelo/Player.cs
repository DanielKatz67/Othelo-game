namespace Ex02_Othelo;

public class Player
{
    private string m_Color;
    private string m_Name;
    private int m_Score;

    public string Color
    {
        get
        {
            return m_Color;
        }
    }
    
    public string Name
    {
        get
        {
            return m_Name;
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

    public Player(string i_Name, int i_Score, string i_Color)
    {
        m_Name = i_Name;
        m_Score = i_Score;
        m_Color = i_Color;
    }
}