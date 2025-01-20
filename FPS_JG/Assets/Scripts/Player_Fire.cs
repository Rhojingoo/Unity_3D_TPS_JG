using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fire : MonoBehaviour
{

    public GameObject Fire_Position;

    public GameObject Bomb_Factory;
    public float Throw_Power = 15f;

    public GameObject Bullet_Effect;
    //ParticleSystem Ps;

    // Start is called before the first frame update
    void Start()
    {
      //  Ps = Bullet_Effect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("왼쪽 클릭한다");
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo))
            {
                Bullet_Effect.transform.position = hitInfo.point;

                GameObject effectObj = Instantiate(Bullet_Effect, hitInfo.point, Quaternion.identity);
                ParticleSystem effectPs = effectObj.GetComponent<ParticleSystem>();
                 

                if (effectPs != null)
                {
                    //EulerAngles를 통째로 대입하는 방법
                    //effectPs.transform.eulerAngles = new Vector3(0f, 90f, 0f);
                    //Quaternion.Euler를 사용하여 대입
                    //effectPs.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                    
                    //카메라를 바라보는 방향으로 회전
                    effectPs.transform.LookAt(Camera.main.transform);
                    effectPs.transform.Rotate(0f, -90f, 0f);
                    effectPs.Play();
                }

               // print("레이 확인 한다");

                //Bullet_Effect.transform.position = hitInfo.point;
                // Ps.Play();
            }

        }
        else if (Input.GetMouseButtonDown(1))
        {
            print("수류탄 발사했다");
            GameObject Bomb = Instantiate(Bomb_Factory);
            Bomb.transform.position = Fire_Position.transform.position;

            Rigidbody rb = Bomb.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * Throw_Power, ForceMode.Impulse);
        }
    }
}
