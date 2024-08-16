namespace Ex02_Othelo;

public class Player
{
    private string m_color;
    private string m_name;
    private int m_score;



    public Player(string i_name, int i_score, string i_color)
    {
        m_name = i_name;
        m_score = i_score;
        m_color = i_color;
    }
}