using UnityEngine;

public class MoveBall : MonoBehaviour {
    public float speed = 30;
    private Vector2 direction;
    Vector2 GetRandomDirection()
{
    float minValue = 0.5f; // Valor mínimo para x e y
    Vector2 randomDirection = new Vector2(
        Random.Range(-1f, 1f) < 0 ? -minValue : minValue,
        Random.Range(-1f, 1f)
    ).normalized;
    return randomDirection;
}
    void RespawnBall()
{
    // Reposiciona a bola no centro
    transform.position = Vector2.zero;

    // Define uma nova direção aleatória
    Vector2 direction = GetRandomDirection();

    // Aplica a nova velocidade
    GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
}
    void Start() {
        // Initial Velocity
        RespawnBall();
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketHeight) => ((ballPos.y - racketPos.y) / racketHeight);

    void OnCollisionEnter2D(Collision2D col) {

        if (collision.gameObject.CompareTag("WallVertical_R")||collision.gameObject.CompareTag("WallVertical_L"))
        {
            // Inverte a direção no eixo Y
            direction.y = -direction.y;
        }
       
        /*

          
          col.gameObject = racket
          col.transform.position = racket's position
          col.collider = racket's collider*/
        if (col.gameObject.name == "Racket_L") {

        //Calcula onde a bola acerta a raquete
        float y = hitFactor(transform.position,
                            col.transform.position,
                            col.collider.bounds.size.y);

        
        Vector2 dir = new Vector2(1, y).normalized; //garantindo velocidade constante

        GetComponent<Rigidbody2D>().linearVelocity = dir * speed;
    }
    if (col.gameObject.name == "Racket_R") {

        float y = hitFactor(transform.position,
                            col.transform.position,
                            col.collider.bounds.size.y);

        
        Vector2 dir = new Vector2(-1, y).normalized;

        GetComponent<Rigidbody2D>().linearVelocity = dir * speed;
    }
    }
}
    
