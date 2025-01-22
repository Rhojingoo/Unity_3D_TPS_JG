using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rotate : MonoBehaviour
{
    public float Rot_Spped = 200f;

    float mx = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Gm.State != GameManager.GameState.Run)
        {
            return;
        }


        float Mouse_x = Input.GetAxis("Mouse X");

        mx += Mouse_x * Rot_Spped * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
