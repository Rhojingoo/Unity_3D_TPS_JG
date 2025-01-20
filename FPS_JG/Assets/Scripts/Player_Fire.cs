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
            print("���� Ŭ���Ѵ�");
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo))
            {
                Bullet_Effect.transform.position = hitInfo.point;

                GameObject effectObj = Instantiate(Bullet_Effect, hitInfo.point, Quaternion.identity);
                ParticleSystem effectPs = effectObj.GetComponent<ParticleSystem>();
                 

                if (effectPs != null)
                {
                    //EulerAngles�� ��°�� �����ϴ� ���
                    //effectPs.transform.eulerAngles = new Vector3(0f, 90f, 0f);
                    //Quaternion.Euler�� ����Ͽ� ����
                    //effectPs.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                    
                    //ī�޶� �ٶ󺸴� �������� ȸ��
                    effectPs.transform.LookAt(Camera.main.transform);
                    effectPs.transform.Rotate(0f, -90f, 0f);
                    effectPs.Play();
                }

               // print("���� Ȯ�� �Ѵ�");

                //Bullet_Effect.transform.position = hitInfo.point;
                // Ps.Play();
            }

        }
        else if (Input.GetMouseButtonDown(1))
        {
            print("����ź �߻��ߴ�");
            GameObject Bomb = Instantiate(Bomb_Factory);
            Bomb.transform.position = Fire_Position.transform.position;

            Rigidbody rb = Bomb.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * Throw_Power, ForceMode.Impulse);
        }
    }
}
