using UnityEngine;
using UnityEngine.UI;
public class Scoreboard : MonoBehaviour
{
    public Sprite[] scoreImages; 
    private Image scoreDisplay;   
    private int score = 0;
    public int Score
    {
        get=> score;
        set
        {
            if(value<=10)
                score = Mathf.Max(0, value);
        } 
    }
    void Start()
    {
        scoreDisplay = GetComponent<Image>();
        UpdateScoreboard();
    }

    public void AddPoint() 
    {
        if (Score < scoreImages.Length - 1) { // Evita erro ao ultrapassar o máximo
            Score++;
            UpdateScoreboard();
        }
    }
    public void ResetPoints(){Score -= Score;}
    
    void UpdateScoreboard() 
    {
        scoreDisplay.sprite = scoreImages[Score]; // Troca a imagem do placar
    }
}
