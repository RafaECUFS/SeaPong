using UnityEngine;

public class MoveBall : MonoBehaviour
{
    [SerializeField]
    private float _speed = 30f; 
    private Vector2 _direction;
    private Rigidbody2D _rb;
    public Canvas canvas;
    private ScoreTrack pointTracker;

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

    public void Update()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (!col.enabled)
        {
            col.enabled = true; // Reativa o trigger caso tenha sido desativado (Respawn não era chamado após colisão)
            Debug.Log("Collider da parede foi reativado!");
        }
    }

    public void Awake()
    {
        Rb = GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D uma vez
        Rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Garante boa detecção de colisão (Houve bugs de colisão antes)
        Rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    public Vector2 GetRandomDirection()
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

    
    public void Respawn()
{
    Debug.Log("Respawn!");

    // Ativa o Collider se estiver desativado
    Collider2D col = GetComponent<Collider2D>();
    if (!col.enabled)
    {
        col.enabled = true;
        Debug.Log("Collider da bola reativado!");
    }

    Rb.linearVelocity = Vector2.zero;

    // Garante que a posição fique dentro dos limites do Canvas
    RectTransform canvasRect = canvas.GetComponent<RectTransform>();
    transform.position = new Vector3(canvasRect.position.x, canvasRect.position.y, 0);

    float randomY = Random.Range(-0.5f, 0.5f);
    transform.position += new Vector3(0, randomY, 0);

    Direction = GetRandomDirection();
    Rb.linearVelocity = Direction * Speed;
}


    public void Start()
    {
    pointTracker = FindObjectOfType<ScoreTrack>();
    if (pointTracker == null)
    {
        Debug.LogError("ScoreTrack não encontrado na cena.");
    }
        Respawn();
    }

    public void OnCollisionEnter2D(Collision2D collision) //Bola rebate das paredes horizontais
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

    public void OnTriggerEnter2D(Collider2D other) //Trigger das paredes laterais ativdo
    {
        if (other.gameObject.CompareTag("Sidewall"))
        {
            Respawn(); // Reseta quando colide com a parede lateral

            if (other.gameObject.name == "Wall_R")
            {
                if (pointTracker != null){
                    pointTracker.RedScore.AddPoint();
                    pointTracker.CheckWinCondition();
                    }

                
            }
            if (other.gameObject.name == "Wall_L")
            {
                if (pointTracker != null)
                {
                    pointTracker.BlueScore.AddPoint();
                    pointTracker.CheckWinCondition();
                    }
                
            }
        }
    }
}