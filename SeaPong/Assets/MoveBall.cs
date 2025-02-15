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
        // Gera um vetor de direção aleatória e normaliza
        Vector2 randomDirection = new Vector2(
            Random.Range(-1.5f, 1.5f),
            Random.Range(-1.5f, 1.5f)
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
            Respawn(); // Reseta quando colide com a parede lateral
        }
    }
}
