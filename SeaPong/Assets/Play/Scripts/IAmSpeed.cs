using UnityEngine;
using System.Collections;

public class IamSpeed : MonoBehaviour
{
    private MoveBall ball; 
    public MoveRacket redBoat;
    public MoveRacket blueBoat;
    private PowerUpManager powerUpManager;

    void Start()
    {
        ball = FindObjectOfType<MoveBall>();
        if (ball == null)
        {
            Debug.LogError("MoveBall não encontrado na cena.");
        }

        redBoat = GameObject.Find("Boat_Red")?.GetComponent<MoveRacket>();
        if (redBoat == null)
        {
            Debug.LogError("MoveRacket_Red não encontrado na cena.");
        }

        blueBoat = GameObject.Find("Boat_Blue")?.GetComponent<MoveRacket>();
        if (blueBoat == null)
        {
            Debug.LogError("MoveRacket_Blue não encontrado na cena.");
        }

        powerUpManager = FindObjectOfType<PowerUpManager>();
        if (powerUpManager == null)
        {
            Debug.LogError("PowerUpManager não encontrado na cena.");
        }
    }

    // Aplica o efeito do PowerUp com duração limitada
    public void PowerUp(MoveRacket boat)
    {
        if (boat != null)
        {
            boat.speedIncrease();
            StartCoroutine(ResetSpeedAfterDelay(boat, 3f)); // Após 3s, reseta a velocidade
        }
        gameObject.SetActive(false); // Em vez de destruir, apenas desativa o power-up
    }

    private IEnumerator ResetSpeedAfterDelay(MoveRacket boat, float delay)
    {
        yield return new WaitForSeconds(delay);
        boat.ResetSpeed();
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
            PowerUp(blueBoat);
        else if (ball.NameCollide == "Boat_Red")
            PowerUp(redBoat);
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
