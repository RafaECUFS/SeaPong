using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pauseText;
    public PainelController panelManager;
    private bool isPaused = false;
    public bool IsPaused
    {
        get => isPaused;
        private set { isPaused = value; }
    }

    void TogglePause() //Alterna o pause
    {
        IsPaused = !IsPaused;
        pausePanel.SetActive(IsPaused);
        pauseText.SetActive(IsPaused);

        Time.timeScale = IsPaused ? 0 : 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !panelManager.GameWon) // Verifica se a tecla ESPAÇO foi pressionada E se o jogo já foi ganho
        {
            TogglePause();
        }
    }
}
