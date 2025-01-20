using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Particle_AutoDisable : MonoBehaviour
{
        ParticleSystem ps;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        // 이 오브젝트 활성화될 때 파티클이 자동 재생되도록
        if (ps != null) ps.Play();
    }

    // 파티클이 모두 재생된 후 호출되는 콜백
    void OnParticleSystemStopped()
    {
        // 풀링 재사용을 위해 비활성화
        gameObject.SetActive(false);
    }
}
