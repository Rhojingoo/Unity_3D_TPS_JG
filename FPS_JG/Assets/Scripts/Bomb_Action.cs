using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Action : MonoBehaviour
{
    public GameObject BombEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject gob = Instantiate(BombEffect);

        gob.transform.position = transform.position;

        Destroy(gameObject);
    }
}
