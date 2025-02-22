using UnityEngine;

public class MoveRacket : MonoBehaviour
{
    public float speed = 30;
    public string axis = "Vertical";
    //Use FixedUpdate for racket physics since Physics are also updated within a fixed interval
     void FixedUpdate()
    {
       float racketDirection = Input.GetAxisRaw(axis);
       GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, racketDirection);
    }
}
