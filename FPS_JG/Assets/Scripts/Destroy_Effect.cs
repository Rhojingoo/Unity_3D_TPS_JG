using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Effect : MonoBehaviour
{

    public float Destroy_Time = 1.5f;
    private float Cur_Time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Cur_Time > Destroy_Time)
        {
            Destroy(gameObject);
        }
        Cur_Time += Time.deltaTime;
    }
}
