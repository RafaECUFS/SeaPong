using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public float speed = 30f;
    public Vector2 direction;
    private Rigidbody2D rb;

    void Update()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (!col.enabled)
        {
            col.enabled = true; // Reativa o trigger caso tenha sido desativado
            Debug.Log("Collider da parede foi reativado!");
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D uma vez
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Garante boa detecção de colisão
    }

    Vector2 GetRandomDirection()
{
    // Define os ângulos mínimo e máximo em radianos
    float minAngle = 30f * Mathf.Deg2Rad; // 30° em radianos
    float maxAngle = 60f * Mathf.Deg2Rad; // 60° em radianos

    // Escolhe aleatoriamente um dos quatro quadrantes
    int quadrant = Random.Range(0, 4); // 0, 1, 2 ou 3

    // Gera um ângulo aleatório dentro do intervalo desejado (30° a 60°)
    float randomAngle = Random.Range(minAngle, maxAngle);

    // Ajusta o ângulo com base no quadrante escolhido
    switch (quadrant)
    {
        case 0: // Primeiro quadrante (30° a 60°)
            break; // Já está correto
        case 1: // Segundo quadrante (120° a 150°)
            randomAngle = 180f * Mathf.Deg2Rad - randomAngle;
            break;
        case 2: // Terceiro quadrante (210° a 240°)
            randomAngle = 180f * Mathf.Deg2Rad + randomAngle;
            break;
        case 3: // Quarto quadrante (300° a 330°)
            randomAngle = 360f * Mathf.Deg2Rad - randomAngle;
            break;
    }

    // Converte o ângulo para um vetor de direção
    Vector2 randomDirection = new Vector2(
        Mathf.Cos(randomAngle),
        Mathf.Sin(randomAngle)
    ).normalized;

    return randomDirection;
}

    void Respawn()
    {
        rb.linearVelocity = Vector2.zero; //direction * speed;
        float randomY = Random.Range(-1.5f, 1.5f);

        rb.MovePosition(new Vector2(0, randomY));
        direction = GetRandomDirection();

        // Aplica a velocidade inicial
        rb.linearVelocity = direction * speed;
    }

    void Start()
    {
        Respawn();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WallBounce"))
        {
            // Inverte a direção no eixo Y
            direction.y = -direction.y;
            rb.linearVelocity = direction * speed;
        }
        if (collision.gameObject.CompareTag("Racket"))
        {
            //calcula onde bateu
            // Inverte a direção no eixo X
            direction.x = -direction.x;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sidewall"))
        {
            Respawn(); // Reseta quando colide com a parede lateral

            if (other.gameObject.name == "Wall_R")
            {
                //Implementar Ponto pro jogador esquerdo
                Debug.Log("Ponto para o jogador esquerdo!");
            }
            if (other.gameObject.name == "Wall_L")
            {
                //Implementar Ponto pro jogador direito
                Debug.Log("Ponto para o jogador direito!");
            }
            
        }
    }
}
