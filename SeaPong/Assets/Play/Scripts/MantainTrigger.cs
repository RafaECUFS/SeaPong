using UnityEngine;

public class MantainTrigger : MonoBehaviour
{
    //Fix do trigger da parede
    void Update()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (!col.enabled)
        {
            col.enabled = true; // Reativa o trigger caso tenha sido desativado
            Debug.Log("Collider da parede foi reativado!");
        }
    }
}
