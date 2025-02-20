using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject pauseText;
    private bool isPaused = false;

    void TogglePause()//Alterna o pause
    {
        isPaused = !isPaused; 
        pausePanel.SetActive(isPaused); 
        pauseText.SetActive(isPaused);
        
        Time.timeScale = isPaused ? 0 : 1;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Verifica se a tecla ESPAÇO foi pressionada
        {
            TogglePause();
        }
    }
}
