using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PainelController : MonoBehaviour
{
    public RectTransform painel; // Arraste o Panel aqui no Inspector
    public GameObject victoryPanel;
    public GameObject MenuButton;
    public TMP_Text victoryText;
    private bool gameWon = false;
    public bool GameWon
    {
        get => gameWon;
        private set { gameWon = value; }
    }


    public void MoverPainel(bool win)
    {
        Vector2 novaPosicao;
        if (win)
            novaPosicao = new Vector2(-430, 100);
        else
            novaPosicao = new Vector2(430, 100);

        painel.anchoredPosition = novaPosicao;
    }

    public void EndGame(string message)
    {
        GameWon = true; // Pausa lógica do jogo
        Time.timeScale = 0; // Garante que o jogo pare
        victoryPanel.SetActive(GameWon);
        MenuButton.SetActive(GameWon);
        if (victoryText != null)
            victoryText.text = message;
    }
}
