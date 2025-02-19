using UnityEngine;

public class MoveBall : MonoBehaviour
{
    [SerializeField]
    private float _speed = 30f; 
    private Vector2 _direction;
    private Rigidbody2D _rb;
    public Canvas canvas;

    public float Speed
    {
        get => _speed;
        set
        {
            // Velocidade >= 0 sempre
            _speed = Mathf.Max(0, value);
        }
    }

    public Vector2 Direction
    {
        get => _direction;
        private set => _direction = value;
    }

    private Rigidbody2D Rb
    {
        get => _rb;
        set => _rb = value;
    }

    void Update()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (!col.enabled)
        {
            col.enabled = true; // Reativa o trigger caso tenha sido desativado (Respawn não era chamado após colisão)
            Debug.Log("Collider da parede foi reativado!");
        }
    }

    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D uma vez
        Rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Garante boa detecção de colisão (Houve bugs de colisão antes)
        Rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    Vector2 GetRandomDirection()
    {
        float minAngle = 30f * Mathf.Deg2Rad;
        float maxAngle = 60f * Mathf.Deg2Rad;

        // Escolhe aleatoriamente um dos quatro quadrantes e um ângulo do intervalo escolhido
        int quadrant = Random.Range(0, 4);
        float randomAngle = Random.Range(minAngle, maxAngle);

        switch (quadrant)
        {
            case 0:
                break;
            case 1:
                randomAngle = 180f * Mathf.Deg2Rad - randomAngle;
                break;
            case 2:
                randomAngle = 180f * Mathf.Deg2Rad + randomAngle;
                break;
            case 3:
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

    
    void Respawn() // Reinicia o objeto
    {
        Debug.Log("Respawn!");
        Rb.linearVelocity = Vector2.zero; 

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();//Centro do Canvas
        transform.position = canvasRect.position;
        float randomY = Random.Range(-0.5f, 0.5f);  
        transform.position += new Vector3(0, randomY, 0);

        Direction = GetRandomDirection();
        Rb.linearVelocity = Direction * Speed; 
    }

    void Start()
    {
        Respawn();
    }

    void OnCollisionEnter2D(Collision2D collision) //Bola rebate das paredes horizontais
    {
        if (collision.gameObject.CompareTag("WallBounce"))
        {
            Direction = new Vector2(Direction.x, -Direction.y);
            Rb.linearVelocity = Direction * Speed; 
        }
        if (collision.gameObject.CompareTag("Racket"))
        {
            Direction = new Vector2(-Direction.x, Direction.y); 
        }
    }

    void OnTriggerEnter2D(Collider2D other) //Trigger das paredes laterais ativdo
    {
        if (other.gameObject.CompareTag("Sidewall"))
        {
            Respawn(); // Reseta quando colide com a parede lateral

            if (other.gameObject.name == "Wall_R")
            {
                GameObject.Find("Score_Red").GetComponent<Scoreboard>().AddPoint();
                Debug.Log("Ponto para o jogador esquerdo!");
            }
            if (other.gameObject.name == "Wall_L")
            {
                GameObject.Find("Score_Blue").GetComponent<Scoreboard>().AddPoint();
                Debug.Log("Ponto para o jogador direito!");
            }
        }
    }
}