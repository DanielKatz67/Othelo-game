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

    public Player(string i_name, int i_score, string i_color)
    {
        m_name = i_name;
        m_score = i_score;
        m_color = i_color;
    }
}