using UnityEngine;

public class ScoreTrack : MonoBehaviour
{
    public Scoreboard RedScore;
    public Scoreboard BlueScore;
    public PainelController winTrophy;
    public Pause pauseManager; // Referência ao sistema de pausa
    private bool winner;
    public bool Winner {get=>winner;set{ winner=value;}}
    public void Start()
    {
        GameObject trophy = GameObject.Find("victoryPanel");
        if (trophy != null)
        {
            winTrophy = trophy.GetComponent<PainelController>();
            if (winTrophy == null)
            {
                Debug.LogError("O componente PainelController não foi encontrado no GameObject 'victoryPanel'.");
            }
        }

        GameObject scoreRedObject = GameObject.Find("Score_Red");
        if (scoreRedObject != null)
        {
            RedScore = scoreRedObject.GetComponent<Scoreboard>();
            if (RedScore == null)
            {
                Debug.LogError("O componente Scoreboard não foi encontrado no GameObject 'Score_Red'.");
            }
        }
        else
        {
            Debug.LogError("GameObject 'Score_Red' não encontrado na cena.");
        }

        GameObject scoreBlueObject = GameObject.Find("Score_Blue");
        if (scoreBlueObject != null)
        {
            BlueScore = scoreBlueObject.GetComponent<Scoreboard>();
            if (BlueScore == null)
            {
                Debug.LogError("O componente Scoreboard não foi encontrado no GameObject 'Score_Blue'.");
            }
        }
        else
        {
            Debug.LogError("GameObject 'Score_Blue' não encontrado na cena.");
        }
    }
    

    public void CheckWinCondition()
    {
        if (RedScore.Score >= 10)
        {
            Winner = true;
            winTrophy.MoverPainel(Winner);
            pauseManager.EndGame("Jogador Vermelho venceu!");
        }
        else if (BlueScore.Score >= 10)
        {
            Winner = false;
            winTrophy.MoverPainel(Winner);
            pauseManager.EndGame("Jogador Azul venceu!");
        }
    }
}