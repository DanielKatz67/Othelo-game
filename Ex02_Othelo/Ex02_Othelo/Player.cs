namespace Ex02_Othelo;

public class Player
{
    private string m_color;
    private string m_name;
    private int m_score;

    public string Color
    {
        get
        {
            return m_color;
        }
    }
    
    public string Name
    {
        get
        {
            return m_name;
        }
    }
    
    public int Score
    {
        get
        {
            return m_score;
        }
        set
        {
            m_score = value;
        }
    }

    public Player(string i_name, int i_score, string i_color)
    {
        m_name = i_name;
        m_score = i_score;
        m_color = i_color;
    }
}