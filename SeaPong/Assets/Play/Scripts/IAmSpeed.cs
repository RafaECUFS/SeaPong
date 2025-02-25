using System.Collections;
using UnityEngine;

public class IamSpeed : MonoBehaviour
{
    private MoveBall ball;
    public MoveRacket redBoat;
    public MoveRacket blueBoat;
    private PowerUpManager powerUpManager;

    void Start()
    {
        ball = FindObjectOfType<MoveBall>();

        redBoat = GameObject.Find("Boat_Red")?.GetComponent<MoveRacket>();

        blueBoat = GameObject.Find("Boat_Blue")?.GetComponent<MoveRacket>();

        powerUpManager = FindObjectOfType<PowerUpManager>();
  
    }

    // Aplica o efeito do PowerUp com duração limitada
    public void PowerUp(MoveRacket boat)
    {
        if (boat != null)
        {
            boat.speedIncrease();
            boat.Invoke("ResetSpeed", 2f); // Após 2s, reseta a velocidade
        }
        gameObject.SetActive(false); // Em vez de destruir, apenas desativa o power-up
    }

    /*private IEnumerator ResetSpeedAfterDelay(MoveRacket boat, float delay)
    {
        yield return new WaitForSeconds(delay);
        boat.ResetSpeed();
    }*/

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
