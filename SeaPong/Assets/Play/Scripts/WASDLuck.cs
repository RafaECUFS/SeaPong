using UnityEngine;

public class WASDLuck : MonoBehaviour
{
    private MoveBall ball; 
    private ScoreTrack pointTracker;
    private PowerUpManager powerUpManager;

    void Start()
    {
        ball = FindObjectOfType<MoveBall>();
        if (ball == null)
        {
            Debug.LogError("MoveBall não encontrado na cena.");
        }

        pointTracker = FindObjectOfType<ScoreTrack>();
        if (pointTracker == null)
        {
            Debug.LogError("ScoreTrack não encontrado na cena.");
        }

        powerUpManager = FindObjectOfType<PowerUpManager>();
        if (powerUpManager == null)
        {
            Debug.LogError("PowerUpManager não encontrado na cena.");
        }
    }

    public void PowerUp(Scoreboard collided, Scoreboard opponent)
    {
        if (collided.Score < opponent.Score)
        {
            opponent.LosePoints(2);
            Debug.Log("Nova pontuação oponente: " + opponent.Score);
        }
        else
        {
            collided.BonusPoints(2);
            Debug.Log("Nova pontuação jogador: " + collided.Score);
        }
            

        gameObject.SetActive(false); // Em vez de destruir, apenas desativa o power-up
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (ball == null)
        {
            Debug.LogError("Ball não encontrado!");
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
