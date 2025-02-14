using UnityEngine;

public class MoveBall : MonoBehaviour {
    public float speed = 30;
    public Vector2 direction;
    private Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D uma vez
    }
    Vector2 GetRandomDirection()
    {
        // Gera um vetor de direção aleatória e normaliza
        Vector2 randomDirection = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)).normalized;
        return randomDirection;
    }
    void Respawn(){
        
        rb.linearVelocity = Vector2.zero; //direction * speed;
        float randomY= Random.Range(-0.5f, 0.5f); 
        
        rb.MovePosition(new Vector2(0, randomY));
        direction = GetRandomDirection();

        // Aplica a velocidade inicial
        rb.linearVelocity = direction * speed;
    }
     void Start() {
        Respawn();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall_Top") || collision.gameObject.CompareTag("Wall_Bottom") )
        {
            // Inverte a direção no eixo Y
            direction.y = -direction.y;
        }
        if (collision.gameObject.CompareTag("RacketL") || collision.gameObject.CompareTag("RacketR"))
        {
            //calcula onde bateu
            // Inverte a direção no eixo X
            direction.x = -direction.x;
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Wall_R")) { 
            //Ponto pro jogador esquerdo
            Respawn(); // Reseta quando colide com a parede lateral
        }
        if (other.CompareTag("Wall_L")) { 
            //Ponto pro jogador direito
            Respawn(); // Reseta quando colide com a parede lateral
        }
    }
}
    
