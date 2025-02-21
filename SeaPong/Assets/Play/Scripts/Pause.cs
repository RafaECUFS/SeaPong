using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pauseText;
    public GameObject victoryPanel;
    public GameObject MenuButton;
    public TMP_Text victoryText;
    private bool isPaused = false;
    public bool IsPaused{get=>isPaused; private set{isPaused=value;} }
    void TogglePause()//Alterna o pause
    {
        IsPaused = !IsPaused; 
        pausePanel.SetActive(IsPaused); 
        pauseText.SetActive(IsPaused);
        
        Time.timeScale = IsPaused ? 0 : 1;
    }

    public void EndGame(string message)
    {
        isPaused = true; // Pausa lógica do jogo
        Time.timeScale = 0; // Garante que o jogo pare
        victoryPanel.SetActive(isPaused);
        MenuButton.SetActive(isPaused);
        if (victoryText!=null)
            victoryText.text = message;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Verifica se a tecla ESPAÇO foi pressionada
        {
            TogglePause();
        }
    }
}
