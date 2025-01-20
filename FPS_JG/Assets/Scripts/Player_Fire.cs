using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fire : MonoBehaviour
{
    public GameObject Fire_Position;

    GameObject[] Bullet_ObjectPool;
    public int Bullet_PoolSize = 10;
    public GameObject Bomb_Factory;
    public float Throw_Power = 15f;

    // --------- (B) ����Ʈ Ǯ ���� -----------
    public GameObject Bullet_Effect;     // ��ƼŬ ������
    public int Effect_PoolSize = 10;     // Ǯ ũ��
    GameObject[] Effect_ObjectPool;
    //ParticleSystem Ps;

    // Start is called before the first frame update
    void Start()
    {
        // (A) �Ѿ�(��ź) Ǯ: ���ø� �ְ�, ���� �� ���� �ִ� ����
        //Bullet_ObjectPool = new GameObject[Bullet_PoolSize];
        //for (int i = 0; i < Bullet_PoolSize; i++)
        //{
        //    GameObject bullet = Instantiate(Bomb_Factory);
        //    bullet.SetActive(false);
        //    Bullet_ObjectPool[i] = bullet;
        //}

        // (B) �淿����Ʈ Ǯ ����
        Effect_ObjectPool = new GameObject[Effect_PoolSize];
        for (int i = 0; i < Effect_PoolSize; i++)
        {
            GameObject effectObj = Instantiate(Bullet_Effect);
            effectObj.SetActive(false);
            Effect_ObjectPool[i] = effectObj;

            // ��ƼŬ�� ��� �Ϸ�Ǹ� �ڵ����� Ǯ�� ��ȯ�ϱ� ���� ��ũ��Ʈ(�Ʒ����� ����)
            //effectObj.AddComponent<ParticleAutoDisable>();
        }
        //  Ps = Bullet_Effect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo = new RaycastHit();


            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject effectObj = GetEffectFromPool();
                if (effectObj != null)
                {
                    // ��ġ/ȸ�� ����
                    effectObj.transform.position = hitInfo.point;

                    // ���ϴ� ȸ�� ó�� (��: ī�޶� ���� ����)
                    effectObj.transform.LookAt(Camera.main.transform);
                    effectObj.transform.Rotate(0f, -90f, 0f);

                    // Ȱ��ȭ�Ͽ� ��ƼŬ ���
                    effectObj.SetActive(true);
                    ParticleSystem ps = effectObj.GetComponent<ParticleSystem>();
                    if (ps != null)
                        ps.Play();
                }




                //Bullet_Effect.transform.position = hitInfo.point;
                //GameObject effectObj = Instantiate(Bullet_Effect, hitInfo.point, Quaternion.identity);
                //ParticleSystem effectPs = effectObj.GetComponent<ParticleSystem>();               

                //if (effectPs != null)
                //{
                //    //EulerAngles�� ��°�� �����ϴ� ���
                //    //effectPs.transform.eulerAngles = new Vector3(0f, 90f, 0f);
                //    //Quaternion.Euler�� ����Ͽ� ����
                //    //effectPs.transform.rotation = Quaternion.Euler(0f, 90f, 0f);


                //    //�浹�� ��ü�� �������� ȸ��(������ �̷��� �ϸ� �ɵ�)
                //    //effectPs.transform.forward = hitInfo.normal;

                //    //ī�޶� �ٶ󺸴� �������� ȸ��(�̹��� ��ü�� ������ ���� �־�� ��������)
                //    effectPs.transform.LookAt(Camera.main.transform);
                //    effectPs.transform.Rotate(0f, -90f, 0f);

                //    effectPs.Play();
                //}

                //// print("���� Ȯ�� �Ѵ�");
                // //Bullet_Effect.transform.position = hitInfo.point;
                // // Ps.Play();
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


    GameObject GetEffectFromPool()
    {
        for (int i = 0; i < Effect_ObjectPool.Length; i++)
        {
            // ���� ��� ���� �ƴ�(��Ȱ��ȭ��) ������Ʈ ã��
            if (!Effect_ObjectPool[i].activeSelf)
            {
                return Effect_ObjectPool[i];
            }
        }
        // �����ִ� ����Ʈ�� ���ٸ�(Ǯ�� �� á�ٸ�),
        // �ʿ信 ���� ���� Instantiate�ϰų�, null�� �����ϴ� �� ��å ����
        // ���⼭�� null ����
        return null;
    }

}
