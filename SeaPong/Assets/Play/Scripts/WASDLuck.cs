using UnityEngine;

public class WASDLuck : MonoBehaviour
{
    private MoveBall ball; 
    private ScoreTrack pointTracker;
    private PowerUpManager powerUpManager;

    void Start()
    {
        ball = FindObjectOfType<MoveBall>();
        pointTracker = FindObjectOfType<ScoreTrack>();
        powerUpManager = FindObjectOfType<PowerUpManager>();
    }

    public void PowerUp(Scoreboard collided, Scoreboard opponent)
    {
        if (collided.Score < opponent.Score)
        {
            opponent.LosePoints(2);
        }
        else
        {
            collided.BonusPoints(2);
        }
            

        gameObject.SetActive(false); // Em vez de destruir, apenas desativa o power-up
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (ball == null)
        {
            return;
        }

        // Aplica o power-up ao último jogador que tocou na bola
        if (ball.NameCollide == "Boat_Blue")
            PowerUp(pointTracker.BlueScore, pointTracker.RedScore);
        else if (ball.NameCollide == "Boat_Red")
            PowerUp(pointTracker.RedScore, pointTracker.BlueScore);
    }

    // Método para reativar o power-up em uma nova posição
    public void RespawnPowerUp()
    {
        if (powerUpManager == null)
            return;

        transform.position = powerUpManager.GetRandomPositionInSpawn();
        gameObject.SetActive(true);
    }
}
