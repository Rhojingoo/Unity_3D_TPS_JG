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
        // �� ������Ʈ Ȱ��ȭ�� �� ��ƼŬ�� �ڵ� ����ǵ���
        if (ps != null) ps.Play();
    }

    // ��ƼŬ�� ��� ����� �� ȣ��Ǵ� �ݹ�
    void OnParticleSystemStopped()
    {
        // Ǯ�� ������ ���� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
