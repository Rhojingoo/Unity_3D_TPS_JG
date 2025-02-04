using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Action : MonoBehaviour
{
    public GameObject BombEffect;
    int Dammage = 50;
    public float explosionRadius = 5f;

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
        Collider[] Cols = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 10);

        for (int i = 0; i < Cols.Length; i++)
        {
            Cols[i].GetComponent<Enermy_FSM>().Hit_Enermy(Dammage);
        }

        GameObject gob = Instantiate(BombEffect);
        gob.transform.position = transform.position;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
