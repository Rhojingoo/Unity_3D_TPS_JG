using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fire : MonoBehaviour
{

    public GameObject Fire_Position;
    public GameObject Types_of_Weapons;
    public GameObject Bomb_Factory;

    public float Throw_Power = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
        else if (Input.GetMouseButtonDown(1))
        {
            GameObject Bomb = Instantiate(Bomb_Factory);
            Bomb.transform.position = Fire_Position.transform.position;

            Rigidbody rb = Bomb.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * Throw_Power, ForceMode.Impulse);
        }
    }
}
