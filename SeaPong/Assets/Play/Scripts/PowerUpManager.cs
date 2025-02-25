using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject powerUp1;
    public GameObject powerUp2;
    public RectTransform spawnArea; // Painel menor dentro do Canvas onde os power-ups devem spawnar
    public Canvas canvas; // Referência ao Canvas principal

    public float chancePowerUp1 = 70f;
    public float chancePowerUp2 = 30f;
    public float rngSpawn = 40f;

    //Gerenciamento de Spawn/Despawn
    private void Start()
    {
        MoveBall.OnBallRespawned += TrySpawnPowerUp;
    }

    private void OnDestroy()
    {
        MoveBall.OnBallRespawned -= TrySpawnPowerUp;
    }

    public Vector3 GetRandomPositionInSpawn() //Spawn aleatório dentro de um panel
    {
        if (spawnArea == null || canvas == null)
        {
            return Vector3.zero;
        }

        // Obtém os limites da área de spawn (em coordenadas locais)
        float minX = -spawnArea.rect.width / 2;
        float maxX = spawnArea.rect.width / 2;
        float minY = -spawnArea.rect.height / 2;
        float maxY = spawnArea.rect.height / 2;

        // Gera uma posição dentro da spawnArea
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(randomX, randomY, 0);
        return spawnPos;
    }

    private void TrySpawnPowerUp() //rng de spawn de powerup
    {

        if (Random.Range(0f, 100f) > rngSpawn)
            return;

        float randomValue = Random.Range(0f, 100f);
        GameObject chosenPowerUp = (randomValue <= chancePowerUp1) ? powerUp1 : powerUp2;

        if (chosenPowerUp != null)
        {
            chosenPowerUp.transform.SetParent(spawnArea, false); // Garante que seja filho do spawnArea
            chosenPowerUp.transform.localPosition = GetRandomPositionInSpawn();
            chosenPowerUp.SetActive(true);
        }

    }
}
