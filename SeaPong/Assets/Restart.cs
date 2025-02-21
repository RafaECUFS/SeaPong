using UnityEngine;

public class Restart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private ScoreTrack pointTracker;
    private MoveBall reposition;
    
    
    public void RestartMatch()
    {
        pointTracker = FindObjectOfType<ScoreTrack>();
        if (pointTracker == null)
        {
            Debug.LogError("ScoreTrack não encontrado na cena.");
        }

        pointTracker.RedScore.ResetPoints();
        pointTracker.BlueScore.ResetPoints();
        
        reposition = FindObjectOfType<MoveBall>();
        if (reposition == null)
        {
            Debug.LogError("MoveBall não encontrado na cena.");
        }
        
        reposition.Respawn();

        Time.timeScale = 1;
    }


}
