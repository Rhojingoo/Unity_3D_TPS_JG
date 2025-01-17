using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cammer_Rotate : MonoBehaviour
{
    public float rotSpeed = 200f;

    float mx = 0f;
    float my = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Mouse_X = Input.GetAxis("Mouse X");
        float Mouse_Y = Input.GetAxis("Mouse Y");

        mx = Mouse_X * rotSpeed * Time.deltaTime;
        my = Mouse_Y * rotSpeed * Time.deltaTime;
        my = Mathf.Clamp(my, -90f, 90);

        transform.eulerAngles += new Vector3(-my, mx,0);

        //Vector3 Dir = new Vector3(-Mouse_Y,Mouse_X, 0);
        
        //transform.eulerAngles += Dir * rotSpeed * Time.deltaTime;

        //Vector3 rot = transform.eulerAngles;
        //rot.x = Mathf.Clamp(rot.x, -90f, 90);
        //transform.eulerAngles = rot;
    }

}
