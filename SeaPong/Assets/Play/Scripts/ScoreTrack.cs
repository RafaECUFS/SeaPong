using UnityEngine;

public class ScoreTrack : MonoBehaviour //Gerencia pontuações
{
    public Scoreboard RedScore;
    public Scoreboard BlueScore;
    public PainelController winTrophy;
    public Pause pauseManager; // Referência ao sistema de pausa
    private bool isWinner;
    public bool IsWinner
    {
        get => isWinner;
        set { isWinner = value; }
    }

    public void Start()
    {
        GameObject trophy = GameObject.Find("victoryPanel");
        if (trophy != null)
        {
            winTrophy = trophy.GetComponent<PainelController>();
        }

        GameObject scoreRedObject = GameObject.Find("Score_Red");
        if (scoreRedObject != null)
        {
            RedScore = scoreRedObject.GetComponent<Scoreboard>();
        }


        GameObject scoreBlueObject = GameObject.Find("Score_Blue");
        if (scoreBlueObject != null)
        {
            BlueScore = scoreBlueObject.GetComponent<Scoreboard>();
        }
    }

    public void CheckWinCondition()
    {
        if (RedScore.Score >= 10)
        {
            IsWinner = true;
            winTrophy.MoverPainel(IsWinner);
            pauseManager.EndGame("Jogador Vermelho venceu!");
        }
        else if (BlueScore.Score >= 10)
        {
            IsWinner = false;
            winTrophy.MoverPainel(IsWinner);
            pauseManager.EndGame("Jogador Azul venceu!");
        }
    }
}
