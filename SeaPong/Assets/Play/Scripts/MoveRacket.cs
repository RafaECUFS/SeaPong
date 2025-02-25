using UnityEngine;

public class MoveRacket : MonoBehaviour
{
    public float speed = 12f;
    public float speedInitial = 12f;
    public float fixedIncrease = 1.5f;
    public string axis = "Vertical";
    private Rigidbody2D racketRigidBody;

    public void ResetSpeed()
    {
        speed = speedInitial;
    }

    void Awake()
    {
        racketRigidBody = GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D uma vez
    }

    //Use FixedUpdate for racket physics since Physics are also updated within a fixed interval
    void FixedUpdate()
    {
        float racketDirection = Input.GetAxisRaw(axis);
        racketRigidBody.linearVelocity = new Vector2(0, racketDirection * speed);
    }

    public void speedIncrease()
    {
        speed *= fixedIncrease;
    }
}
