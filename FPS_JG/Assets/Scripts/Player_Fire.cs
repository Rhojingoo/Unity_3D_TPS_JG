using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class Player_Fire : MonoBehaviour
{
    public GameObject Fire_Position;
    Animator Anim;

    //--------- (A) 총알(수류탄) 풀 관련 -----------
    GameObject[] Bullet_ObjectPool;
    public int Bullet_PoolSize = 10;
    public GameObject Bomb_Factory;
    public float Throw_Power = 15f;


    // --------- (B) 이펙트 풀 관련 ----------
    public GameObject Bullet_Effect;     // 파티클 프리팹
    public int Effect_PoolSize = 10;     // 풀 크기
    GameObject[] Effect_ObjectPool;
    public int Bullet_Power = 15;
    //ParticleSystem Ps;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();

        // (A) 총알(폭탄) 풀: 예시만 있고, 아직 안 쓰고 있는 상태
        //Bullet_ObjectPool = new GameObject[Bullet_PoolSize];
        //for (int i = 0; i < Bullet_PoolSize; i++)
        //{
        //    GameObject bullet = Instantiate(Bomb_Factory);
        //    bullet.SetActive(false);
        //    Bullet_ObjectPool[i] = bullet;
        //}

        // (B) 이펙트 풀 생성
        Effect_ObjectPool = new GameObject[Effect_PoolSize];
        for (int i = 0; i < Effect_PoolSize; i++)
        {
            GameObject effectObj = Instantiate(Bullet_Effect);
            effectObj.SetActive(false);
            Effect_ObjectPool[i] = effectObj;

            // 파티클이 재생 완료되면 자동으로 풀에 반환하기 위한 스크립트
            //effectObj.AddComponent<ParticleAutoDisable>();
        }
        //  Ps = Bullet_Effect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Gm.State != GameManager.GameState.Run)
        {
            return;        
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Anim.GetFloat("MoveMotion") == 0)
            {
                Anim.SetTrigger("ATTACK");
            }


            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo = new RaycastHit();


            if (Physics.Raycast(ray, out hitInfo))
            {

                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enermy"))
                {
                    print("플레이어 총솼어");
                    Enermy_FSM Monster = hitInfo.transform.GetComponent<Enermy_FSM>();
                    Monster.Hit_Enermy(Bullet_Power);
                }
                else 
                {
                    GameObject effectObj = GetEffectFromPool();
                    if (effectObj != null)
                    {
                        // 위치/회전 설정
                        effectObj.transform.position = hitInfo.point;

                        // 원하는 회전 처리 (예: 카메라 방향 보정)
                        effectObj.transform.LookAt(Camera.main.transform);
                        effectObj.transform.Rotate(0f, -90f, 0f);

                        // 활성화하여 파티클 재생
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
                    //    //EulerAngles를 통째로 대입하는 방법
                    //    //effectPs.transform.eulerAngles = new Vector3(0f, 90f, 0f);
                    //    //Quaternion.Euler를 사용하여 대입
                    //    //effectPs.transform.rotation = Quaternion.Euler(0f, 90f, 0f);


                    //    //충돌된 물체의 법선벡터 회전(보통은 이렇게 하면 될듯)
                    //    //effectPs.transform.forward = hitInfo.normal;

                    //    //카메라를 바라보는 방향으로 회전(이미지 자체가 옆으로 돌아 있어서서 설정변경)
                    //    effectPs.transform.LookAt(Camera.main.transform);
                    //    effectPs.transform.Rotate(0f, -90f, 0f);

                    //    effectPs.Play();
                    //}

                    //// print("레이 확인 한다");
                    // //Bullet_Effect.transform.position = hitInfo.point;
                    // // Ps.Play();
                }





            }

        }
        else if (Input.GetMouseButtonDown(1))
        {
            print("수류탄 발사");
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
            // 아직 사용 중이 아닌(비활성화된) 오브젝트 찾기
            if (!Effect_ObjectPool[i].activeSelf)
            {
                return Effect_ObjectPool[i];
            }
        }
        // 남아있는 이펙트가 없다면(풀이 꽉 찼다면),
        // 필요에 따라 새로 Instantiate하거나, null을 리턴하는 등 정책 결정
        // 여기서는 null 리턴
        return null;
    }

}
