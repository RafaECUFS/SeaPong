using UnityEngine;

public class PowerUps : MonoBehaviour
{

    private int rngSpawn, rngPower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rngSpawn =  Random.Range(0,4);
        rngPower = Random.Range(0,2000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
