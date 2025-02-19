using UnityEngine;

public class MoveRacket : MonoBehaviour
{
    public float speed = 30;
    public string axis = "Vertical";
    private Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D uma vez
    }
    //Use FixedUpdate for racket physics since Physics are also updated within a fixed interval
     void FixedUpdate()
    {
       float racketDirection = Input.GetAxisRaw(axis);
       rb.linearVelocity = new Vector2(0, racketDirection*speed);
    }
}
